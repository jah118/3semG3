using DataAccess.Models;
using System.Linq;

namespace DataAccess.DataTransferObjects.Converters
{
    public partial class Converter
    {
        public static UserDTO Convert(User u)
        {
            Customer customer = u.Person.Customer.SingleOrDefault(c => c.PersonId == u.PersonId);
            Employee employee = u.Person.Employee.SingleOrDefault(c => c.PersonId == u.PersonId);
            UserRoles role = UserRoles.Undefined;
            if (customer != null)
            {
                role = UserRoles.Customer;
            }
            //Employee overrides the Role as a customer
            if (employee != null)
            {
                role = UserRoles.Employee;
            }
            return new UserDTO
            {
                Customer = Convert(customer),
                Employee = Convert(employee),
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