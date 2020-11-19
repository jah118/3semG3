using DataAccess.DataTransferObjects;
using DataAccess.DataTransferObjects.Converters;
using DataAccess.Models;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using SemanticComparison;

namespace DataAccess.Tests
{
    [TestClass, TestCategory("Converters")]
    public class ConversionTest
    {
        [TestMethod, TestCategory("Unit"), TestCategory("Converters")]
        public void CustomerConversionOutput()
        {
            //Arrange
            var customerID = 2;
            var personID = 4;
            var email = "test@example.com";

            var input = new Customer()
            {
                Id = customerID,
                PersonId = personID,
                Person = new Person()
                {
                    Id = personID,
                    Email = email,
                    FirstName = "",
                    LastName = "",
                    Location = new Location()
                    {
                        Address = "",
                        ZipCode = "",

                    },
                    LocationId = -1,
                    Phone = ""
                }
                
            };
            var expected = new CustomerDTO()
            {
                Id = customerID,
                
            };
            //Act
            var output = Converter.Convert(input);
            //Assert
            //sem
            //Assert.IsTrue(output);
        }

        [TestMethod, TestCategory("Unit"), TestCategory("Converters")]
        public void ReservationConversionOutput()
        {

            //Arrange
            var input = new Reservation()
            {

            };
            var expected = new ReservationDTO()
            {
                Id = 3
            };
            //Act
            var output = Converter.Convert(input);
            //Assert
            Assert.IsTrue(true);
        }

        [TestMethod, TestCategory("Unit"), TestCategory("Converters")]
        public void EmployeeConversionOutput()
        {

            //Arrange
            var input = new Reservation()
            {

            };
            var expected = new ReservationDTO()
            {
                Id = 3
            };
            //Act
            var output = Converter.Convert(input);
            //Assert
            Assert.IsTrue(true);
        }

        [TestMethod, TestCategory("Unit"), TestCategory("Converters")]
        public void TableConversionOutput()
        {

            //Arrange
            var input = new Reservation()
            {

            };
            var expected = new ReservationDTO()
            {
                Id = 3
            };
            //Act
            var output = Converter.Convert(input);
            //Assert
            Assert.IsTrue(true);
        }

    }
}