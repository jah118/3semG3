using DataAccess.DataTransferObjects;

namespace RestaurantAPI.Models
{
    public record LoginInfo
    {
        public UserDTO User { get; init; }
        public string Username { get; init; }
        public string Password { get; init; }
        public UserRoles Role { get; init; }
    }
}