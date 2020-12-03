using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.DataTransferObjects
{
    public class UserDTO
    {
        public int Id { get; init; }
        public string Username { get; init; }
        public CustomerDTO Customer { get; init; }
        public EmployeeDTO Employee { get; init; }
        public UserRoles AccountType {get; init;}
    }
}
