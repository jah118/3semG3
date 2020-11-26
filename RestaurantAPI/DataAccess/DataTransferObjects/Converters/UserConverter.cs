using DataAccess.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.DataTransferObjects.Converters
{
    public partial class Converter
    {
        public static UserDTO Convert(User u)
        {
            Customer customer = null;
            Employee employee = null;
            if (u.Person.Customer.SingleOrDefault(c => c.PersonId == u.PersonId) != null)
            {

            }
            if (u.Person.Employee.SingleOrDefault(c => c.PersonId == u.PersonId) != null)
            {

            }
            return new UserDTO
            {
                
            };
        }

        public static User Convert(UserDTO u)
        {
            return new User();
        }
    }
}
