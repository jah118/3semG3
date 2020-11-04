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
    public class CustomerRepository : IRepository<CustomerDTO>
    {
        private readonly IDbConnection _connection;

        public CustomerRepository(RestaurantContext context)
        {

        }

        public CustomerDTO Create(CustomerDTO obj)
        {
            throw new NotImplementedException();
        }

        public bool Delete(CustomerDTO obj)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<CustomerDTO> GetAll()
        {
            throw new NotImplementedException();
        }

        public CustomerDTO GetById(int id)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<CustomerDTO> GetCountWithOffsetByOrdering(int count, int offset, string ordering)
        {
            List<Customer2> res = null;

            throw new NotImplementedException();
        }

        public CustomerDTO Update(CustomerDTO obj)
        {
            throw new NotImplementedException();
        }
    }
}
