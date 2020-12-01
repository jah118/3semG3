using DataAccess.DataTransferObjects;
using DataAccess.DataTransferObjects.Converters;
using DataAccess.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using Microsoft.EntityFrameworkCore.ChangeTracking;

namespace DataAccess.Repositories
{
    public class CustomerRepository : IRepository<CustomerDTO>
    {
        private readonly RestaurantContext _context;

        public CustomerRepository(RestaurantContext context)
        {
            _context = context;
        }

        public CustomerDTO Create(CustomerDTO obj, bool transactionEndpoint = true)
        {
            if (transactionEndpoint) _context.Database.BeginTransaction(IsolationLevel.Serializable);
            var added = CreateCustomer(obj);
            _context.SaveChanges();
            if (transactionEndpoint)_context.Database.CommitTransaction();
            return Converter.Convert(added.Entity);
        }

        internal EntityEntry<Customer> CreateCustomer(CustomerDTO obj)
        {
            return _context.Add(Converter.Convert(obj));
        }

        public bool Delete(CustomerDTO obj, bool transactionEndpoint = true)
        {
            if (transactionEndpoint) _context.Database.BeginTransaction(IsolationLevel.Serializable);
            //insert logic here
            _context.SaveChanges();
            if (transactionEndpoint)_context.Database.CommitTransaction();
            throw new NotImplementedException();
        }

        public IEnumerable<CustomerDTO> GetAll()
        {
            var customers = _context.Customer
                .Include(c => c.Person)
                    .ThenInclude(c => c.Location)
                        .ThenInclude(c => c.ZipCodeNavigation)
                .AsNoTracking()
                .ToList()
                    ;

            var res = new List<CustomerDTO>();
            foreach (Customer c in customers)
            {
                res.Add(Converter.Convert(c));
            }

            return res;
        }

        public CustomerDTO GetById(int id)
        {
            CustomerDTO res = null;
            Customer customer = _context.Customer
                            .Where(c => c.Id == id)
                            .Include(c => c.Person)
                            .ThenInclude(c => c.Location)
                            .ThenInclude(c => c.ZipCodeNavigation)
                            .AsNoTracking()
                            .FirstOrDefault();
            if (customer != null)
            {
                res = Converter.Convert(customer);
            }

            return res;
        }

        public IEnumerable<CustomerDTO> GetCountWithOffsetByOrdering(int count, int offset, string ordering)
        {
            //  List<Customer> res = null;

            throw new NotImplementedException();
        }

        public CustomerDTO Update(CustomerDTO obj, bool transactionEndpoint = true)
        {
            if (transactionEndpoint) _context.Database.BeginTransaction(IsolationLevel.Serializable);
            //insert logic here
            if (transactionEndpoint) _context.SaveChanges();
            throw new NotImplementedException();
        }
    }
}