using DataAccess.DataTransferObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RestaurantAPI.Authentication
{
    public interface IAuthManager
    {
        string Authenticate(string username, string password, UserRoles role);
    }
}
