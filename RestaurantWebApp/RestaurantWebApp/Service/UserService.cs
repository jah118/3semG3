using Newtonsoft.Json;
using RestaurantWebApp.DataTransferObject;
using RestaurantWebApp.Service.Interfaces;
using RestSharp;

namespace RestaurantWebApp.Service
{
    public class UserService : IUserService
    {
        private readonly string _conString;
        private readonly IAuthService _authService;

        public UserService(string constring, IAuthService authService)
        {
            _conString = constring;
            _authService = authService;
        }

        public UserDTO GetUserByUsername(string username)
        {
            UserDTO res = null;
            var client = new RestClient(_conString);
            var request = new RestRequest("/User/info/{username}", Method.GET);
            request.AddUrlSegment("username", username);

            if (_authService.AddTokenToRequest(request))
            {
                var content = client.Execute(request).Content;
                res = JsonConvert.DeserializeObject<UserDTO>(content);
            }

            return res;
        }

        public UserDTO GetUserById(int id)
        {
            UserDTO res = null;
            var client = new RestClient(_conString);
            var request = new RestRequest("/User/{id}", Method.GET);
            request.AddUrlSegment("id", id);

            if (_authService.AddTokenToRequest(request))
            {
                var content = client.Execute(request).Content;
                res = JsonConvert.DeserializeObject<UserDTO>(content);
            }
            return res;
        }
    }
}