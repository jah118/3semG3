using DataAccess.DataTransferObjects;
using DataAccess.Repositories.Interfaces;
using DataAccess.Utility;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Repositories
{
    public class UserRepository : IAccountRepository
    {
        public UserDTO Create(UserDTO obj, bool transactionEndpoint = true)
        {
            //Create User with option of adding password later
            throw new NotImplementedException();
        }

        public UserDTO Create(UserDTO obj, string password, bool transactionEndpoint = true)
        {
            var result = PasswordHashing.CreateHash(password);
            throw new NotImplementedException();
        }

        public bool Delete(UserDTO obj, bool transactionEndpoint = true)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<UserDTO> GetAll()
        {
            throw new NotImplementedException();
        }

        public UserDTO GetById(int id)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<UserDTO> GetCountWithOffsetByOrdering(int count, int offset, string ordering)
        {
            throw new NotImplementedException();
        }

        public UserDTO Update(UserDTO obj, bool transactionEndpoint = true)
        {
            throw new NotImplementedException();
        }
    }
}
