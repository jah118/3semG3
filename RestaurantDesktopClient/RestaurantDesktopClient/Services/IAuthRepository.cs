using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataAccess.DataTransferObjects;
using RestSharp;

namespace RestaurantDesktopClient.Services
{
    public interface IAuthRepository
    {
        bool Authenticate(string username, string password);
        bool Create(EmployeeDTO employee, string username, string password);
        bool AddTokenToRequest(RestRequest request);
    }
}
