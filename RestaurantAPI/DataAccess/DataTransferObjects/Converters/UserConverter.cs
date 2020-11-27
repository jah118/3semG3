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
            Customer customer = u.Person.Customer.SingleOrDefault(c => c.PersonId == u.PersonId);
            Employee employee = u.Person.Employee.SingleOrDefault(c => c.PersonId == u.PersonId);
            UserRoles role = UserRoles.Customer;
            if (employee != null)
            {
                role = UserRoles.Employee;
            }
            return new UserDTO
            {
                Customer = Convert(customer),
                Employee= Convert(employee),
                AccountType = role,
                Id = u.Id,
                Username = u.Username

            };
        }

        public static User Convert(UserDTO u)
        {
            return new User()
            {
                
            };
        }
    }
}
