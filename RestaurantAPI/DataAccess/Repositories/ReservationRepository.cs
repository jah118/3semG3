﻿using DataAccess.DataTransferObjects;
using DataAccess.DataTransferObjects.Converters;
using DataAccess.Models;
using DataAccess.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;

namespace DataAccess.Repositories
{
    public class ReservationRepository : IReservationRepository
    {
        private readonly RestaurantContext _context;

        public ReservationRepository(RestaurantContext context)
        {
            _context = context;
        }

        public ReservationDTO Create(ReservationDTO obj, bool transactionEndpoint = true)
        {
            if (Reservation.Validate(obj))
            {
                //Sanity check here, ensure unique tables etc.
                if (transactionEndpoint) _context.Database.BeginTransaction(IsolationLevel.Serializable);
                try
                {
                    var tableIDs = obj.Tables.Select(t => t.Id);
                    var compare = _context.Reservation
                        .Include(r => r.ReservationsTables)
                        .ThenInclude(r => r.RestaurantTables)
                        .Where(r =>
                            r.ReservationTime <= obj.ReservationTime.AddMinutes(90) &&
                            r.ReservationTime.AddMinutes(90) >= obj.ReservationTime &&
                            r.ReservationsTables.Any(i => tableIDs.Contains(i.RestaurantTablesId)));

                    if (compare.Count() == 0)
                    {
                        var reservation = Converter.Convert(obj);
                        reservation.ReservationDate = DateTime.Now;
                        if (obj.Customer.Id == 0)
                        {
                            var isCustomer = _context.Customer.Include(c => c.Person)
                                .ThenInclude(c => c.Location)
                                .ThenInclude(c => c.ZipCodeNavigation)
                                .Where(c => c.Person.Phone
                                    .Equals(obj.Customer.Phone))
                                .FirstOrDefault();

                            if (isCustomer == null)
                            {
                                var customerToAdd = new CustomerRepository(_context).CreateCustomer(obj.Customer);
                                reservation.Customer = customerToAdd.Entity;
                            }
                            else
                            {
                                reservation.Customer = isCustomer;
                            }
                        }

                        _context.Reservation.Add(reservation);
                        foreach (var rt in obj.Tables)
                            _context.Add(new ReservationsTables
                            {
                                Reservation = reservation,
                                RestaurantTablesId = rt.Id
                            });

                        _context.SaveChanges();
                        if (transactionEndpoint) _context.Database.CommitTransaction();
                        _context.Entry(reservation).GetDatabaseValues();
                        return GetById(reservation.Id);
                    }

                    if (transactionEndpoint) _context.Database.RollbackTransaction();
                }
                catch (Exception)
                {
                    _context.Database.RollbackTransaction();
                    throw;
                }
            }
            return null;
        }

        public bool Delete(int id, bool transactionEndpoint = true)
        {
            if (transactionEndpoint) _context.Database.BeginTransaction(IsolationLevel.RepeatableRead);
            {
                var reservation = _context.Reservation
                    .Where(r => r.Id == id)
                    .Include(c => c.Customer)
                    .ThenInclude(c => c.Person)
                    .ThenInclude(c => c.Location)
                    .ThenInclude(c => c.ZipCodeNavigation)
                    .Include(rt => rt.ReservationsTables)
                    .ThenInclude(t => t.RestaurantTables)
                    .AsNoTracking()
                    .FirstOrDefault();

                if (reservation != null)
                {
                    _context.Entry(reservation).State = EntityState.Modified;
                    var order = _context.RestaurantOrder.FirstOrDefault(o => o.ReservationId == id);
                    try
                    {
                        //update order to cancel and remove tables
                        if (order != null)
                        {
                            var or = new OrderRepository(_context);
                            if (or.cancelOrder(order.OrderNo))
                            {
                                // remove tables
                                _context.ReservationsTables.RemoveRange
                                    (_context.ReservationsTables.Where(r => r.ReservationId == reservation.Id));
                                if (transactionEndpoint)
                                {
                                    _context.SaveChanges();
                                    _context.Database.CommitTransaction();
                                    return true;
                                }
                            }
                            return false;
                        }

                        _context.Remove(reservation);

                        if (transactionEndpoint)
                        {
                            _context.SaveChanges();
                            _context.Database.CommitTransaction();
                        }

                        return true;

                    }
                    catch (Exception)
                    {
                        _context.Database.RollbackTransaction();
                        throw;
                    }
                }
            }

            return false;
        }

        public IEnumerable<ReservationDTO> GetAll()
        {
            IEnumerable<ReservationDTO> res = null;

            var reservations = _context.Reservation
                .Include(c => c.Customer)
                .ThenInclude(c => c.Person)
                .ThenInclude(c => c.Location)
                .ThenInclude(c => c.ZipCodeNavigation)
                .Include(rt => rt.ReservationsTables)
                .ThenInclude(t => t.RestaurantTables).AsNoTracking().ToList();

            if (reservations != null) res = Converter.Convert(reservations);

            return res;
        }

        public ReservationDTO GetById(int id)
        {
            ReservationDTO res = null;
            var reservation = _context.Reservation
                .Where(r => r.Id == id)
                .Include(c => c.Customer)
                .ThenInclude(c => c.Person)
                .ThenInclude(c => c.Location)
                .ThenInclude(c => c.ZipCodeNavigation)
                .Include(rt => rt.ReservationsTables)
                .ThenInclude(t => t.RestaurantTables)
                .AsNoTracking()
                .SingleOrDefault();
            if (reservation != null) res = Converter.Convert(reservation);
            return res;
        }

        public IEnumerable<ReservationDTO> GetCountWithOffsetByOrdering(int count, int offset, string ordering)
        {
            throw new NotImplementedException();
        }

        public ReservationDTO Update(ReservationDTO obj, bool transactionEndpoint = true)
        {
            if (Reservation.Validate(obj))
            {
                if (transactionEndpoint) _context.Database.BeginTransaction(IsolationLevel.Serializable);
                try
                {
                    var refReservation = _context.ReservationsTables.Where(i => i.ReservationId == obj.Id).ToList();

                    //Dette tjekker båder efter om tid og bordet er ændre til et sted/tid der skaber conflict
                    var compareCount = _context.Reservation
                        .Include(r => r.ReservationsTables)
                        .ThenInclude(r => r.RestaurantTables)
                        .Where(r =>
                            r.ReservationTime <= obj.ReservationTime.AddMinutes(90) &&
                            r.ReservationTime.AddMinutes(90) >= obj.ReservationTime
                            && r.Id != obj.Id)
                        .Select(r => r.ReservationsTables
                            .Where(t => t.RestaurantTablesId
                                .Equals(obj.Tables.Select(table => table.Id).Any()))).Count();

                    if (compareCount == 0)
                    {
                        var reservation = Converter.Convert(obj);
                        var tablesToAdd = obj.Tables
                            .Where(
                                t => refReservation.All(rt => rt.RestaurantTablesId != t.Id)).ToList();
                        var tablesToDelete =
                            refReservation.Where(
                                t => obj.Tables.All(td => t.RestaurantTablesId != td.Id)).ToList();
                        foreach (var rt in tablesToDelete) _context.ReservationsTables.Remove(rt);

                        foreach (var rt in tablesToAdd)
                            _context.Add(new ReservationsTables
                            {
                                Reservation = reservation,
                                RestaurantTablesId = rt.Id
                            });

                        reservation.Id = obj.Id;
                        _context.Entry(reservation).State = EntityState.Modified;

                        if (transactionEndpoint)
                        {
                            _context.SaveChanges();
                            _context.Entry(reservation).State = EntityState.Detached;
                            _context.Database.CommitTransaction();
                        }

                        return GetById(obj.Id);
                    }
                }
                catch (Exception)
                {
                    _context.Database.RollbackTransaction();
                    throw;
                }
            }

            return null;
        }

        public AvailableTimesDTO GetReservationTimeByDateAndTime(DateTime dateTime)
        {
            var timeSlots = new AvailableTimesDTO
            { AvailabilityDate = dateTime.Date, TableOpenings = new List<AvailableTimesDTO.TableTimes>() };
            var startTime = new TimeSpan(12, 00, 00);
            var endTime = new TimeSpan(22, 00, 00);

            var startDateTime = dateTime.Date + startTime;
            var endDateTime = dateTime.Date + endTime;

            var all = _context.Reservation
                .Include(r => r.ReservationsTables)
                .ThenInclude(r => r.RestaurantTables)
                .Where(r =>
                    r.ReservationTime <= endDateTime &&
                    r.ReservationTime >= startDateTime).AsNoTracking();
            var tables = new TableRepository(_context).GetAll();
            var availabilityList = new List<AvailableTimesDTO.TableTimes>();
            foreach (var table in tables)
            {
                var tableTime = new AvailableTimesDTO.TableTimes { Table = table };
                var timesAvailable = new List<AvailableTimesDTO.TableTimes.TimePair>();
                var times = all.Where(r => r.ReservationsTables.Any(t => t.RestaurantTablesId == table.Id))
                    .Select(r => r.ReservationTime).OrderBy(t => t.TimeOfDay);
                var lastTime = startDateTime;
                foreach (var time in times)
                {
                    timesAvailable.Add(new AvailableTimesDTO.TableTimes.TimePair { Start = lastTime, End = time });
                    lastTime = time.AddMinutes(90);
                }

                if (lastTime.AddMinutes(90) <= endDateTime)
                    timesAvailable.Add(
                        new AvailableTimesDTO.TableTimes.TimePair { Start = lastTime, End = endDateTime });
                tableTime.Openings = timesAvailable;
                availabilityList.Add(tableTime);
            }

            timeSlots.TableOpenings = availabilityList;

            return timeSlots;
        }

        public IEnumerable<ReservationDTO> ReservationsByCustomerUsername(string tokenUserName)
        {
            IEnumerable<ReservationDTO> res = null;
            UserRepository repository = new UserRepository(_context);
            var user = repository.GetUserWithToken(tokenUserName);
            if (user.Customer != null)
            {
                var reservation = _context.Reservation
                    .Where(r => r.CustomerId == user.Customer.Id)
                    .Include(c => c.Customer)
                    .ThenInclude(c => c.Person)
                    .ThenInclude(c => c.Location)
                    .ThenInclude(c => c.ZipCodeNavigation)
                    .Include(rt => rt.ReservationsTables)
                    .ThenInclude(t => t.RestaurantTables)
                    .AsNoTracking()
                    .ToList();
                if (reservation.Count > 0) res = Converter.Convert(reservation);
            }
      
            return res;
        }
    }
}