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
        public string Password { get; set; }
        public  CustomerDTO customer { get; set; }

     
      
    }
}
