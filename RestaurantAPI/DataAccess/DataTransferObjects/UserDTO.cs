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
        public CustomerDTO Customer { get; set; }
        public EmployeeDTO Employee { get; set; }
        public UserRoles AccountType {get; init;}
    }
}
