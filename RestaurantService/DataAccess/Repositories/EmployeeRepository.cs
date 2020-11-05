﻿using DataAccess.DataTransferObjects;
using DataAccess.Models;
using System;
using System.Collections.Generic;
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

        public EmployeeDTO Create(EmployeeDTO obj)
        {
            throw new NotImplementedException();
        }

        public bool Delete(EmployeeDTO obj)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<EmployeeDTO> GetAll()
        {
            var employees = _context.Employee.ToList();
            var res = new List<EmployeeDTO>();
            foreach (Employee e in employees)
            {
                res.Add(new EmployeeDTO
                {
                    FirstName = e.Id.ToString()
                });
            }

            return res;
        }

        public EmployeeDTO GetById(int id)
        {
            return new EmployeeDTO { FirstName = "Lars", LastName = "Nysom", Address = "vej vej 8", City = "Ållern", Email = "la@nysom.dk", Phone = "22222222", ZipCode = "9000" };
        }

        public IEnumerable<EmployeeDTO> GetCountWithOffsetByOrdering(int count, int offset, string ordering)
        {
            List<Employee> res = null;

            throw new NotImplementedException();
        }

        public EmployeeDTO Update(EmployeeDTO obj)
        {
            throw new NotImplementedException();
        }
    }
}