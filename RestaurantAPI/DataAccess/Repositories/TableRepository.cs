using DataAccess.DataTransferObjects;
using DataAccess.DataTransferObjects.Converters;
using System;
using System.Collections.Generic;
using System.Linq;
using DataAccess.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;

namespace DataAccess.Repositories
{
    public class TableRepository : IRepository<RestaurantTablesDTO>
    {
        private readonly RestaurantContext _context;

        public TableRepository(RestaurantContext context)
        {
            _context = context;
        }

        public RestaurantTablesDTO Create(RestaurantTablesDTO obj, bool transactionEndpoint = true)
        {
            throw new NotImplementedException();
        }

        internal EntityEntry<RestaurantTables> CreateTable(RestaurantTablesDTO obj)
        {
            throw new NotImplementedException();
        }

        public bool Delete(RestaurantTablesDTO obj, bool transactionEndpoint = true)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<RestaurantTablesDTO> GetAll()
        {
            IEnumerable<RestaurantTablesDTO> res = null;
            var restaurantTables = _context.RestaurantTables.Include(rt => rt.ReservationsTables).AsNoTracking().ToList();
            if (restaurantTables != null)
            {
                res = Converter.Convert(restaurantTables);
            }

            return res;
        }

        public RestaurantTablesDTO GetById(int id)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<RestaurantTablesDTO> GetCountWithOffsetByOrdering(int count, int offset, string ordering)
        {
            throw new NotImplementedException();
        }

        public RestaurantTablesDTO Update(RestaurantTablesDTO obj, bool transactionEndpoint = true)
        {
            throw new NotImplementedException();
        }
    }
}