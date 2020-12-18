using DataAccess.DataTransferObjects;
using DataAccess.DataTransferObjects.Converters;
using DataAccess.Models;
using DataAccess.Repositories.Interfaces;
using DataAccess.Utility;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;

namespace DataAccess.Repositories
{
    public class UserRepository : IAccountRepository
    {
        private readonly RestaurantContext _context;

        public UserRepository(RestaurantContext context)
        {
            _context = context;
        }

        public UserDTO Create(UserDTO obj, bool transactionEndpoint = true)
        {
            //Create User with option of adding password later
            throw new NotImplementedException();
        }

        public UserDTO Create(UserDTO obj, string password, bool transactionEndpoint = true)
        {
            if (transactionEndpoint) _context.Database.BeginTransaction(IsolationLevel.Serializable);
            var (hash, salt) = PasswordHashing.CreateHash(password);
            //Validate
            if (User.Validate(obj))
            {
                var person = obj.AccountType switch
                {
                    UserRoles.Customer => new CustomerRepository(_context).CreateCustomer(obj.Customer).Entity.Person,
                    UserRoles.Employee => new EmployeeRepository(_context).CreateEmployee(obj.Employee).Entity.Person,
                    _ => null,
                };

                if (person != null)
                {
                    var toAddUser = new User()
                    {
                        Username = obj.Username,
                        Person = person,
                        PasswordHash = hash,
                        Salt = salt
                    };

                    _context.Add(toAddUser);
                    _context.SaveChanges();
                    _context.Entry(toAddUser).GetDatabaseValues();
                    if (transactionEndpoint)_context.Database.CommitTransaction();
                    return GetById(toAddUser.Id);
                }
            }
            if (transactionEndpoint) _context.Database.RollbackTransaction();

            return null;
        }

        public bool Delete(int id, bool transactionEndpoint = true)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<UserDTO> GetAll()
        {
            throw new NotImplementedException();
        }

        public UserDTO GetById(int id)
        {
            var user = _context.User
                .Where(u => u.Id == id)
                .Include(u => u.Person)
                .ThenInclude(c => c.Customer)
                .Include(e => e.Person.Employee)
                .ThenInclude(e => e.Title)
                .Include(p => p.Person.Location)
                    .ThenInclude(c => c.ZipCodeNavigation)
                .AsNoTracking()
                .FirstOrDefault();


            return Converter.Convert(user);
        }

        public IEnumerable<UserDTO> GetCountWithOffsetByOrdering(int count, int offset, string ordering)
        {
            throw new NotImplementedException();
        }

        public UserDTO Update(UserDTO obj, bool transactionEndpoint = true)
        {
            throw new NotImplementedException();
        }
    }
}