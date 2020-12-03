using DataAccess.DataTransferObjects;
using DataAccess.Repositories.Interfaces;
using DataAccess.Utility;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;

namespace DataAccess.Repositories
{
    public class AuthRepository : IAuthRepository
    {
        private readonly RestaurantContext _context;

        public AuthRepository(RestaurantContext context)
        {
            _context = context;
        }

        public bool AuthenticateUser(string username, string password, UserRoles role)
        {
            var user = _context.User.Include(u => u.Person).FirstOrDefault(u => u.Username == username);
            if (user == null) return false;
            var accurateRole = role switch
            {
                UserRoles.Customer => _context.Customer.Any(customer => customer.PersonId == user.PersonId),
                UserRoles.Employee => _context.Employee.Any(employee => employee.PersonId == user.PersonId),
                _ => false
            };

            var inputHash = PasswordHashing.Hash(password, user.Salt);

            var sameHash = inputHash.SequenceEqual(user.PasswordHash);

            return accurateRole && sameHash;

        }
    }
}