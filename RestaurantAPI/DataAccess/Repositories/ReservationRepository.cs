using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using DataAccess.DataTransferObjects;
using DataAccess.DataTransferObjects.Converters;
using DataAccess.Models;
using DataAccess.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;

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

                    var compareCount = _context.Reservation
                        .Include(r => r.ReservationsTables)
                        .ThenInclude(r => r.RestaurantTables)
                        .Where(r =>
                            r.ReservationTime <= obj.ReservationTime.AddMinutes(90) &&
                            r.ReservationTime.AddMinutes(90) >= obj.ReservationTime)
                        .Select(r => r.ReservationsTables
                            .Where(t => t.RestaurantTablesId
                                .Equals(obj.Tables.Select(table => table.Id).Any()))).Count();

                    if (compareCount == 0)
                    {




                        var reservation = Converter.Convert(obj);
                        reservation.ReservationDate = DateTime.Now;
                        if (obj.Customer.Id == 0)
                        {
                            //TODO does this need be here ?  is there better place for it ?
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
                        foreach (RestaurantTablesDTO rt in obj.Tables)
                        {
                            _context.Add<ReservationsTables>(new ReservationsTables
                            {
                                Reservation = reservation,
                                RestaurantTablesId = rt.Id
                            });
                        }

                        _context.SaveChanges();
                        if (transactionEndpoint) _context.Database.CommitTransaction();
                        _context.Entry(reservation).GetDatabaseValues();
                        return GetById(reservation.Id);
                    }
                    else
                    {
                        if (transactionEndpoint) _context.Database.RollbackTransaction();
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

        public bool Delete(ReservationDTO obj, bool transactionEndpoint = true)
        {
            if (transactionEndpoint) _context.Database.BeginTransaction(IsolationLevel.Serializable);
            //insert logic here
            if (transactionEndpoint) _context.SaveChanges();
            if (transactionEndpoint) _context.Database.CommitTransaction();
            throw new NotImplementedException();
        }

        public IEnumerable<ReservationDTO> GetAll(DateTime startDate, DateTime endDate)
        {
            IEnumerable<ReservationDTO> res = null;
            //TODO clear this and can this be made where only takes reservationdat 
            //startDate = DateTime.Now + new TimeSpan(08,00,00);
            // endDate = startDate.AddDays(15);
            //Where(p => p.ReservationDate.Date >= startDate && p.ReservationDate.Date <= endDate

            var reservations = _context.Reservation.Where(d => d.ReservationTime >= startDate && d.ReservationTime <= endDate)
                .Include(c => c.Customer)
                .ThenInclude(c => c.Person)
                .ThenInclude(c => c.Location)
                .ThenInclude(c => c.ZipCodeNavigation)
                .Include(rt => rt.ReservationsTables)
                .ThenInclude(t => t.RestaurantTables).AsNoTracking().ToList();

            if (reservations != null)
            {
                res = Converter.Convert(reservations);
            }
            return res;
        }

        public AvailableTimesDTO GetReservationTimeByDate(DateTime dateTime)
         {
            var timeSlots = new AvailableTimesDTO() {AvailabilityDate = dateTime.Date, TableOpenings = new List<AvailableTimesDTO.TableTimes>()};
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
                var tableTime = new AvailableTimesDTO.TableTimes() {Table = table};
                var timesAvailable = new List<AvailableTimesDTO.TableTimes.TimePair>();
                var times = all.Where(r => r.ReservationsTables.Any(t => t.RestaurantTablesId == table.Id))
                    .Select(r => r.ReservationTime).OrderBy(t => t.TimeOfDay);
                var lastTime = startDateTime;
                foreach (var time in times)
                {
                    timesAvailable.Add(new AvailableTimesDTO.TableTimes.TimePair() {Start = lastTime, End = time});
                    lastTime = time.AddMinutes(90);
                }

                if (lastTime.AddMinutes(90) <= endDateTime)
                    timesAvailable.Add(
                        new AvailableTimesDTO.TableTimes.TimePair() {Start = lastTime, End = endDateTime});
                tableTime.Openings = timesAvailable;
                availabilityList.Add(tableTime);
            }

            timeSlots.TableOpenings = availabilityList;

            return timeSlots;
        }
        public AvailableTimesDTO GetReservationTimeByDateAndTime(DateTime dateTime)
        {
            var timeSlots = new AvailableTimesDTO() { AvailabilityDate = dateTime.Date, TableOpenings = new List<AvailableTimesDTO.TableTimes>() };
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
                var tableTime = new AvailableTimesDTO.TableTimes() { Table = table };
                var timesAvailable = new List<AvailableTimesDTO.TableTimes.TimePair>();
                var times = all.Where(r => r.ReservationsTables.Any(t => t.RestaurantTablesId == table.Id))
                    .Select(r => r.ReservationTime).OrderBy(t => t.TimeOfDay);
                var lastTime = startDateTime;
                foreach (var time in times)
                {
                    timesAvailable.Add(new AvailableTimesDTO.TableTimes.TimePair() { Start = lastTime, End = time });
                    lastTime = time.AddMinutes(90);
                }

                if (lastTime.AddMinutes(90) <= endDateTime)
                    timesAvailable.Add(
                        new AvailableTimesDTO.TableTimes.TimePair() { Start = lastTime, End = endDateTime });
                tableTime.Openings = timesAvailable;
                availabilityList.Add(tableTime);
            }

            timeSlots.TableOpenings = availabilityList;

            return timeSlots;
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
                .FirstOrDefault();
            if (reservation != null) res = Converter.Convert(reservation);
            return res;
        }

        public IEnumerable<ReservationDTO> GetCountWithOffsetByOrdering(int count, int offset, string ordering)
        {
            throw new NotImplementedException();
        }

        public ReservationDTO Update(ReservationDTO obj, bool transactionEndpoint = true)
        {
            if (transactionEndpoint) _context.Database.BeginTransaction(IsolationLevel.Serializable);
            //insert logic here
            if (transactionEndpoint) _context.SaveChanges();
            throw new NotImplementedException();
        }

        internal EntityEntry<Reservation> CreateReservation(ReservationDTO obj)
        {
            throw
                new NotImplementedException(); //Move logic from Create method to here and return method from the other
        }
    }
}