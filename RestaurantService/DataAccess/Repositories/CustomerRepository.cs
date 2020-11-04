using DataAccess.DataTransferObjects;
using DataAccess.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace DataAccess.Repositories
{
    public class CustomerRepository : IRepository<DTOCustomer>
    {
        private readonly IDbConnection _connection;

        public CustomerRepository(RestaurantContext context)
        {

        }

        public DTOCustomer Create(DTOCustomer obj)
        {
            throw new NotImplementedException();
        }

        public bool Delete(DTOCustomer obj)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<DTOCustomer> GetAll()
        {
            throw new NotImplementedException();
        }

        public DTOCustomer GetById(int id)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<DTOCustomer> GetCountWithOffsetByOrdering(int count, int offset, string ordering)
        {
            List<Customer2> res = null;

            throw new NotImplementedException();
        }

        public DTOCustomer Update(DTOCustomer obj)
        {
            throw new NotImplementedException();
        }
    }
}
