using System;
using System.IdentityModel.Tokens.Jwt;
using System.Web;
using RestaurantWebApp.DataTransferObject;
using RestaurantWebApp.Service.Interfaces;
using RestSharp;

namespace RestaurantWebApp.Service
{
    public class AuthorizationService : IAuthService
    {
        private readonly string _conString;
        private string _token;

        public AuthorizationService(string constring)
        {
            _conString = constring;
        }

        public string Authenticate(string username, string password)
        {
            var client = new RestClient(_conString);
            var request = new RestRequest("/User/Authenticate", Method.POST);
            request.AddJsonBody(new {Username = username, Password = password, Role = "Customer"});
            var response = client.Execute(request);
            if (response.IsSuccessful)
            {
                //Set token field, substring is because the returned json is in quotes,
                _token = response.Content.Substring(1, response.Content.Length - 2);
                HttpContext.Current.Session.Add("APIToken", _token);

                return _token;
            }

            return null;
        }

        public bool Create(CustomerDTO customer, string username, string password)
        {
            var client = new RestClient(_conString);

            var request = new RestRequest("/user/post", Method.POST);
            request.AddJsonBody(new
            {
                Username = username,
                Password = password,
                User = new UserDTO
                {
                    AccountType = UserRoles.Customer,
                    Customer = customer,
                    Username = username
                }
            });
            var response = client.Execute(request);
            if (response.IsSuccessful)
            {
                _token = response.Content;
                return true;
            }

            return false;
        }

        public bool AddTokenToRequest(RestRequest request)
        {
           string apiToken = HttpContext.Current.Session["APIToken"] + "";
            if (apiToken == null) return false;
            var handler = new JwtSecurityTokenHandler();
            var token = handler.ReadJwtToken(apiToken);
            if (token.ValidTo.ToUniversalTime() < DateTime.UtcNow) return false;
            request.AddHeader("Authorization", "Bearer " + apiToken);
            return true;
        }
    }
}