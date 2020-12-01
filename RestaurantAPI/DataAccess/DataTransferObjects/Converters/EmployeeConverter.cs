using DataAccess.Models;

namespace DataAccess.DataTransferObjects.Converters
{
    public partial class Converter
    {
        public static EmployeeDTO Convert(Employee employee)
        {
            return new EmployeeDTO
            {
                Id = employee.Id,
                Title = employee.Title.Title,
                Phone = employee.Person.Phone,
                Email = employee.Person.Email,
                FirstName = employee.Person.FirstName,
                LastName = employee.Person.LastName,
                Address = employee.Person.Location.Address,
                ZipCode = employee.Person.Location.ZipCodeNavigation.ZipCode,
                City = employee.Person.Location.ZipCodeNavigation.City
            };
        }

        public static Employee Convert(EmployeeDTO c)
        {
            return new Employee()
            {
                Title = new EmployeeTitle()
                {
                    Title = c.Title
                },
                Person = new Person()
                {
                    Email = c.Email,
                    FirstName = c.FirstName,
                    LastName = c.LastName,
                    Phone = c.Phone,
                    Location = new Location()
                    {
                        Address = c.Address,
                        ZipCode = c.ZipCode,
                    }
                }
            };
        }
    }
}