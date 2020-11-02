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
    public class CustomerRepository : IRepository<Customer2>
    {
        private readonly IDbConnection _connection;

        public CustomerRepository(string connectionString)
        {
            _connection = new SqlConnection(connectionString);
            //_connectionString = connectionString;
        }

        public Customer2 Create(Customer2 obj)
        {
            throw new NotImplementedException();
        }

        public bool Delete(Customer2 obj)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Customer2> GetAll()
        {
            throw new NotImplementedException();
        }

        public Customer2 GetById(int id)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Customer2> GetCountWithOffsetByOrdering(int count, int offset, string ordering)
        {
            List<Customer2> res = null;

            throw new NotImplementedException();
        }

        public Customer2 Update(Customer2 obj)
        {
            throw new NotImplementedException();
        }
    }
}
