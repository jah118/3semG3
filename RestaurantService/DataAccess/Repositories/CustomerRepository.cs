using DataAccess.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Repositories
{
    public class CustomerRepository : IRepository<Customer>
    {
        private readonly string _connectionString;

        public CustomerRepository(string connectionString)
        {
            _connectionString = connectionString;
        }

        public Customer Create(Customer obj)
        {
            throw new NotImplementedException();
        }

        public bool Delete(Customer obj)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Customer> GetAll()
        {
            throw new NotImplementedException();
        }

        public Customer GetById(int id)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Customer> GetCountWithOffset(int count, int offset)
        {
            throw new NotImplementedException();
        }

        public Customer Update(Customer obj)
        {
            throw new NotImplementedException();
        }
    }
}
