using RestaurantWebApp.DataTransferObject;

namespace RestaurantWebApp.Service.Interfaces
{
    public interface IUserService
    {
        UserDTO GetUserByToken();
        UserDTO GetUserById(int id);
    }
}