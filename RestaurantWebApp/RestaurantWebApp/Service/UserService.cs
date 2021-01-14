using Newtonsoft.Json;
using RestaurantWebApp.DataTransferObject;
using RestaurantWebApp.Service.Interfaces;
using RestSharp;

namespace RestaurantWebApp.Service
{
    public class UserService : IUserService
    {
        private readonly IAuthService _authService;
        private readonly string _conString;

        public UserService(string constring, IAuthService authService)
        {
            _conString = constring;
            _authService = authService;
        }

        public UserDTO GetUserByToken()
        {
            UserDTO res = null;
            var client = new RestClient(_conString);
            var request = new RestRequest("/User/Info", Method.GET);
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
                var content = client.Execute(request);
                var code = content.StatusCode;
                res = JsonConvert.DeserializeObject<UserDTO>(content.Content);
            }

            return res;
        }
    }
}