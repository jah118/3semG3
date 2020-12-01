using DataAccess.DataTransferObjects;
using DataAccess.DataTransferObjects.Converters;
using DataAccess.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;

namespace DataAccess.Repositories
{
    public class EmployeeRepository : IRepository<EmployeeDTO>
    {
        private readonly RestaurantContext _context;

        public EmployeeRepository(RestaurantContext context)
        {
            _context = context;
        }

        public EmployeeDTO Create(EmployeeDTO obj, bool transactionEndpoint = true)
        {
            if (transactionEndpoint) _context.Database.BeginTransaction(IsolationLevel.Serializable);
            var added = CreateEmployee(obj);
            _context.SaveChanges();
            if (transactionEndpoint) _context.Database.CommitTransaction();
            return Converter.Convert(added.Entity);
        }

        internal EntityEntry<Employee> CreateEmployee(EmployeeDTO obj)
        {
            return _context.Add(Converter.Convert(obj));
        }

        public bool Delete(EmployeeDTO obj, bool transactionEndpoint = true)
        {
            if (transactionEndpoint) _context.Database.BeginTransaction(IsolationLevel.Serializable);
            //insert logic here
            if (transactionEndpoint) _context.SaveChanges();
            if (transactionEndpoint)_context.Database.CommitTransaction();
            throw new NotImplementedException();
        }

        public IEnumerable<EmployeeDTO> GetAll()
        {
            var employees = _context.Employee
                .Include(e => e.Person)
                .ThenInclude(e => e.Location)
                .ThenInclude(e => e.ZipCodeNavigation)
                .Include(e => e.Title)
                .AsNoTracking().ToList(); //remember to include every time an additional . is added in the create part

            return employees.Select(Converter.Convert).ToList();
        }

        public EmployeeDTO GetById(int id)
        {
            EmployeeDTO res = null;
            var employee = _context.Employee
                 .Where(c => c.Id == id)
                 .Include(c => c.Person)
                 .ThenInclude(c => c.Location)
                 .ThenInclude(c => c.ZipCodeNavigation)
                 .Include(e => e.Title)
                 .FirstOrDefault();

            if (employee != null)
            {
                res = Converter.Convert(employee);
            }
            return res;
        }

        public IEnumerable<EmployeeDTO> GetCountWithOffsetByOrdering(int count, int offset, string ordering)
        {
            //List<Employee> res = null;

            throw new NotImplementedException();
        }

        public EmployeeDTO Update(EmployeeDTO obj, bool transactionEndpoint = true)
        {
            if (transactionEndpoint) _context.Database.BeginTransaction(IsolationLevel.Serializable);
            //insert logic here
            if (transactionEndpoint) _context.SaveChanges();
            throw new NotImplementedException();
        }
    }
}