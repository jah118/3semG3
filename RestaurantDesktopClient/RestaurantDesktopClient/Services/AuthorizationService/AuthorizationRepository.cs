using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RestaurantDesktopClient.Services.AuthorizationService
{
    class AuthorizationRepository
    {
        private readonly string _constring;
        public AuthorizationRepository(string constring)
        {
            _constring = constring;
        }
    }
}
