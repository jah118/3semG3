using DataAccess.DataTransferObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using RestaurantDesktopClient.DataTransferObject;
using RestSharp;
using System.IdentityModel.Tokens.Jwt;

namespace RestaurantDesktopClient.Services.AuthorizationService
{
    class AuthorizationRepository : IAuthRepository
    {

        private readonly string _constring;
        private string _token;
        public AuthorizationRepository(string constring)
        {
            _constring = constring;
        }

        public bool Authenticate(string username, string password)
        {
            var client = new RestClient(_constring);
            var request = new RestRequest("/User/Authenticate", Method.POST);
            request.AddJsonBody(new {Username = username, Password = password, Role = "Employee"});
            var response = client.Execute(request);
            if(response.IsSuccessful) {
                //Set token field, substring is because the returned json is in quotes, 
                _token = response.Content.Substring(1, response.Content.Length-2);
                return true;
            }
            return false;
        }

        public bool Create(EmployeeDTO employee, string username, string password)
        {
            var client = new RestClient(_constring);

            var request = new RestRequest("/user/post", Method.POST);
            request.AddJsonBody(new
            {
                Username = username,
                Password = password,
                User = new UserDTO()
                {
                    AccountType = "Employee",
                    Employee = employee,
                    Username = username,
                }
            });
            var response = client.Execute(request);
            if(response.IsSuccessful) {
                _token = response.Content;
                return true;
            }

            return false;
        }

        public bool AddTokenToRequest(RestRequest request)
        {
            if (_token == null) return false;
            var handler = new JwtSecurityTokenHandler();
            var token = handler.ReadJwtToken(_token);
            if (token.ValidTo.ToUniversalTime() < DateTime.UtcNow) return false;
            request.AddHeader("Authorization", "Bearer " + _token);
            return true;
        }
    }
}
