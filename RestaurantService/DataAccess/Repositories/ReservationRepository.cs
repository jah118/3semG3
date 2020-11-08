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
    internal class ReservationRepository : IRepository<ReservationDTO>
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
                    .Where(r => r.ReservationsTables
                        .Select(rt => rt.RestaurantTablesId)
                        .Intersect(obj.Tables.Select(table => table.Id)).ToList().Count() > 0)
                    .Count()
                    ;
                if (compareList > 0)
                {
                    var toAdd = Converter.Convert(obj);
                    _context.Add<Reservation>(toAdd).GetDatabaseValues();
                    _context.Entry(toAdd).GetDatabaseValues();
                    var id = toAdd.Id;
                    foreach (RestaurantTablesDTO rt in obj.Tables)
                    {
                        _context.Add<ReservationsTables>(new ReservationsTables
                        {
                            ReservationId = id,
                            RestaurantTablesId = rt.Id
                        });
                    }
                    if (transactionEndpoint) _context.SaveChanges();
                    return GetById(id);
                }
                else
                {
                    if (transactionEndpoint) _context.Database.RollbackTransaction();
                }
            }
            catch (Exception)
            {
                _context.Database.RollbackTransaction();
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
            throw new NotImplementedException();
        }

        public ReservationDTO GetById(int id)
        {
            throw new NotImplementedException();
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