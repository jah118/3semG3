using RestaurantWebApp.DataTransferObject;
using RestSharp;

namespace RestaurantWebApp.Service.Interfaces
{
    public interface IAuthService
    {
        string Authenticate(string username, string password);

        bool Create(CustomerDTO customer, string username, string password);

        bool AddTokenToRequest(RestRequest request);

        UserDTO GetUser(string username);
    }
}