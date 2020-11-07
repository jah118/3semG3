using DataAccess.Models;
using System.Collections.Generic;

namespace DataAccess.DataTransferObjects
{
    public class EmployeeDTO
    {
        public EmployeeDTO()
        {
        }

        public EmployeeDTO(int id)
        {
            Id = id;
        }

        public EmployeeDTO(Employee employee) : this(employee.Id)
        {
            Title = employee.Title.Title;
            Phone = employee.Person.Phone;
            Email = employee.Person.Email;
            FirstName = employee.Person.FirstName;
            LastName = employee.Person.LastName;
            Address = employee.Person.Location.Address;
            ZipCode = employee.Person.Location.ZipCodeNavigation.ZipCode;
            City = employee.Person.Location.ZipCodeNavigation.City;
            //RestaurantOrder = restaurantOrder;
        }

        public int Id { get; }
        public string Title { get; set; }
        public string Phone { get; set; }
        public string Email { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Address { get; set; }
        public string ZipCode { get; set; }
        public string City { get; set; }

        public ICollection<RestaurantOrderDTO> RestaurantOrder { get; set; }
    }
}