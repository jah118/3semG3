using DataAccess.DataTransferObjects;
using DataAccess.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;

namespace DataAccess.Repositories
{
    public class CustomerRepository : IRepository<CustomerDTO>
    {
        private readonly RestaurantContext _context;

        public CustomerRepository(RestaurantContext context)
        {
            _context = context;
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
            var customers = _context.Customer.Include("Person");
            var res = new List<CustomerDTO>();
            foreach (Customer c in customers)
            {
                res.Add(new CustomerDTO(c.Id)
                {
                    FirstName = c.Person.FirstName,
                    LastName = c.Person.LastName
                });
            }

            return res;
        }

        public CustomerDTO GetById(int id)
        {
            return new CustomerDTO { FirstName = "Lars", LastName = "Nysom", Address = "vej vej 8", City = "Ållern", Email = "la@nysom.dk", Phone = "22222222", ZipCode = "9000" };
        }

        public IEnumerable<CustomerDTO> GetCountWithOffsetByOrdering(int count, int offset, string ordering)
        {
            List<Customer> res = null;

            throw new NotImplementedException();
        }

        public CustomerDTO Update(CustomerDTO obj)
        {
            throw new NotImplementedException();
        }
    }
}