using DataAccess.DataTransferObjects;
using DataAccess.Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

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
            return true;
            var user =_context.User.Include(u => u.Person).FirstOrDefault(user => user.Username == username);

            bool accurateRole = role switch
            {
                UserRoles.Customer => _context.Customer.Any(customer => customer.PersonId == user.PersonId),
                UserRoles.Employee => _context.Employee.Any(employee => employee.PersonId == user.PersonId),
                _ => false
            };

            return true;
        }

    }
}
