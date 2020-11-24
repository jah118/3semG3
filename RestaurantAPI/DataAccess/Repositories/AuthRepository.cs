using DataAccess.DataTransferObjects;
using DataAccess.Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
        }

        public byte[] GetSigningKey()
        {
            throw new NotImplementedException();
        }
    }
}
