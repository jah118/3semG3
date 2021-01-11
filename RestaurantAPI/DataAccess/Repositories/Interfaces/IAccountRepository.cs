using DataAccess.DataTransferObjects;

namespace DataAccess.Repositories.Interfaces
{
    public interface IAccountRepository : IRepository<UserDTO>
    {
        public UserDTO Create(UserDTO obj, string password, bool transactionEndpoint = true);
        public UserDTO GetByUserName(string username);
    }
}