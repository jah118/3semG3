using DataAccess.DataTransferObjects;
using DataAccess.DataTransferObjects.Converters;
using DataAccess.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using Microsoft.EntityFrameworkCore.ChangeTracking;

namespace DataAccess.Repositories
{
    public class ReservationRepository : IRepository<ReservationDTO>
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

        internal EntityEntry<Reservation> CreateReservation(ReservationDTO obj)
        {
            throw new NotImplementedException(); //Move logic from Create method to here and return method from the other
        }

        public bool Delete(ReservationDTO obj, bool transactionEndpoint = true)
        {
            if (transactionEndpoint) _context.Database.BeginTransaction(IsolationLevel.Serializable);
            //insert logic here
            if (transactionEndpoint) _context.SaveChanges();
            if (transactionEndpoint) _context.Database.CommitTransaction();
            throw new NotImplementedException();
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

            if (reservations != null)
            {
                res = Converter.Convert(reservations);
            }

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
            if (reservation != null)
            {
                res = Converter.Convert(reservation);
            }
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
    }
}