using DataAccess.Models;
using System;
using System.Collections.Generic;

namespace DataAccess.DataTransferObjects.Converters
{
    public partial class Converter
    {
        public static CustomerDTO Convert(Customer c)
        {
            return new CustomerDTO
            {
                Id = c.Id,
                Phone = c.Person.Phone,
                Email = c.Person.Email,
                FirstName = c.Person.FirstName,
                LastName = c.Person.LastName,
                Address = c.Person.Location.Address,
                ZipCode = c.Person.Location.ZipCodeNavigation.ZipCode,
                City = c.Person.Location.ZipCodeNavigation.City
            };
        }

        public static Customer Convert(CustomerDTO c)
        {
            return new Customer()
            {
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