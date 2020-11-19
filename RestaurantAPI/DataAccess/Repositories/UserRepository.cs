using DataAccess.DataTransferObjects;
using DataAccess.Utility;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Repositories
{
    public class UserRepository : IRepository<UserDTO>
    {
        public UserDTO Create(UserDTO obj, bool transactionEndpoint = true)
        {
            var passRes = PasswordHashing.CreateHash(""); // insert obj.pass
            var hash = passRes.hash;
            var salt = passRes.salt;

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
