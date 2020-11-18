using DataAccess.DataTransferObjects;
using DataAccess.DataTransferObjects.Converters;
using DataAccess.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;

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
            if (transactionEndpoint) _context.Database.BeginTransaction(IsolationLevel.Serializable);
            //Sanity check here, ensure unique tables etc.
            try
            {
                var tableIds = obj.Tables.Select(t => t.Id);
                //Caution O(n^2) or greater, this performs badly, it currently only checks if ANY table intersects
                //Change to focus on tables, return list of the ones that intersect and give it back to the the caller,
                //to tell which ones need rebooking or don't and handle failure in another way.
                var compareList = _context.Reservation
                    .Include(r => r.ReservationsTables)
                    .ThenInclude(r => r.RestaurantTables)
                    .Where(r =>
                        r.ReservationTime <= obj.ReservationTime.AddMinutes(90) &&
                        r.ReservationTime.AddMinutes(90) >= obj.ReservationTime)
                    .Select(r => r.ReservationsTables.Where(t => t.RestaurantTablesId.Equals(obj.Tables.Select(table => table.Id).Any()))).Count();




                    //    .Select(rt => rt.RestaurantTablesId)
                    //    .Intersect(obj.Tables.Select(table => table.Id)).Count() > 0)
                    //.Count()
                    //;

                if (compareList == 0)
                {
                    var toAdd = Converter.Convert(obj);
                    _context.Add<Reservation>(toAdd).GetDatabaseValues();
                    _context.Entry(toAdd).GetDatabaseValues();
                    foreach (RestaurantTablesDTO rt in obj.Tables)
                    {
                        _context.Add<ReservationsTables>(new ReservationsTables
                        {
                            Reservation = toAdd,
                            RestaurantTablesId = rt.Id
                        });
                    }
                    if (transactionEndpoint) _context.SaveChanges();
                    _context.Entry(toAdd).GetDatabaseValues();
                    return GetById(toAdd.Id);
                }
                else
                {
                    if (transactionEndpoint) _context.Database.RollbackTransaction();
                }
            }
            catch (Exception e)
            {
                _context.Database.RollbackTransaction();
                throw;
            }
            return null;
        }

        public bool Delete(ReservationDTO obj, bool transactionEndpoint = true)
        {
            if (transactionEndpoint) _context.Database.BeginTransaction(IsolationLevel.Serializable);
            //insert logic here
            if (transactionEndpoint) _context.SaveChanges();
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
                                .ThenInclude(t => t.RestaurantTables).ToList();

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