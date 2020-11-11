using DataAccess.DataTransferObjects;
using DataAccess.DataTransferObjects.Converters;
using System;
using System.Collections.Generic;
using System.Linq;

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

        public bool Delete(RestaurantTablesDTO obj, bool transactionEndpoint = true)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<RestaurantTablesDTO> GetAll()
        {
            Converter.Convert(_context.RestaurantTables.ToList());
            return Converter.Convert(_context.RestaurantTables.ToList());

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