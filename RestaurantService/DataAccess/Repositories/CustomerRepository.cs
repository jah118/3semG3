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
            var customers = _context.Customer
                .Include(c => c.Person)
                    .ThenInclude(c => c.Location)
                        .ThenInclude(c => c.ZipCodeNavigation)
                    ;

            var res = new List<CustomerDTO>();
            foreach (Customer c in customers)
            {
                res.Add(new CustomerDTO(c.Id)
                {
                    FirstName = c.Person.FirstName,
                    LastName = c.Person.LastName,
                    Email = c.Person.Email,
                    Phone = c.Person.Phone,
                    Address = c.Person.Location.Address,
                    City = c.Person.Location.ZipCodeNavigation.City,
                    ZipCode = c.Person.Location.ZipCodeNavigation.ZipCode
                });
            }

            return res;
        }

        public CustomerDTO GetById(int id)
        {
            Customer customer = _context.Customer
                            .Where(c => c.Id == id)
                            .Include(c => c.Person)
                            .ThenInclude(c => c.Location)
                            .ThenInclude(c => c.ZipCodeNavigation).FirstOrDefault();

            var res = new CustomerDTO(customer);
            return res;
        }

        public IEnumerable<CustomerDTO> GetCountWithOffsetByOrdering(int count, int offset, string ordering)
        {
            //  List<Customer> res = null;

            throw new NotImplementedException();
        }

        public CustomerDTO Update(CustomerDTO obj)
        {
            throw new NotImplementedException();
        }
    }
}