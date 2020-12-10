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

            var request = new RestRequest("/user/authenticate", Method.POST);
            request.AddParameter("username", username, ParameterType.RequestBody);
            request.AddParameter("password", password, ParameterType.RequestBody);
            request.AddParameter("role", "employee", ParameterType.RequestBody);
            var content = client.Execute(request).Content;

            var res = JsonConvert.DeserializeObject<string>(content);
            return false;
        }

        public bool Create(EmployeeDTO employee, string username, string password)
        {
            var client = new RestClient(_constring);

            var request = new RestRequest("/user/post", Method.POST);
            request.AddParameter("username", username, ParameterType.RequestBody);
            request.AddParameter("password", password, ParameterType.RequestBody);
            request.AddParameter("user", new UserDTO()
                {
                    AccountType = "Employee",
                    Employee = employee,
                    Username = username,
                }, ParameterType.RequestBody
            );
            var response = client.Execute(request);
            if(response.IsSuccessful) {
                _token = JsonConvert.DeserializeObject<string>(response.Content);
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
