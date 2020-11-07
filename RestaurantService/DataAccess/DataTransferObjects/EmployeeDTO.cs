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
            Title = employee.Person.e;
            Phone = phone;
            Email = email;
            FirstName = firstName;
            LastName = lastName;
            Address = address;
            ZipCode = zipCode;
            City = city;
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