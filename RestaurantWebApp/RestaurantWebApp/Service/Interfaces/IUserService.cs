using RestaurantWebApp.DataTransferObject;

namespace RestaurantWebApp.Service.Interfaces
{
    public interface IUserService
    {
        UserDTO GetUserByUsername(string username);
        UserDTO GetUserById(int id);

    }
}