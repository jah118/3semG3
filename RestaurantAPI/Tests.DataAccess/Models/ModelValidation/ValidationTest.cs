using DataAccess.DataTransferObjects;
using DataAccess.Models;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Tests.DataAccess.Models.ModelValidation
{
    [TestClass]
    [TestCategory("Validation")]
    public class ValidationTest
    {
        [TestMethod]
        public void CustomerValidationPass()
        {
            //Arrange
            var c = new CustomerDTO
            {
                FirstName = "Alpha",
                LastName = "Beta",
                Address = "Gammavej 4",
                City = "Epsilonby",
                Email = "Alphabeta@example.com",
                ZipCode = "1234",
                Phone = "88888888"
            };
            //Act
            var pass = Customer.Validate(c);
            //Assert
            Assert.IsTrue(pass);
        }

        [TestMethod]
        public void CustomerValidationWithNullFail()
        {
            //Arrange
            var c = new CustomerDTO
            {
                FirstName = "Alpha",
                LastName = "Beta",
                Address = null,
                City = "Epsilonby",
                Email = "Alphabeta@example.com",
                ZipCode = "1234",
                Phone = "88888888"
            };
            //Act
            var pass = Customer.Validate(c);
            //Assert
            Assert.IsFalse(pass);
        }

        [TestMethod]
        public void CustomerValidationWithEmptyStringFail()
        {
            //Arrange
            var c = new CustomerDTO
            {
                FirstName = "Alpha",
                LastName = "Beta",
                Address = "",
                City = "Epsilonby",
                Email = "Alphabeta@example.com",
                ZipCode = "1234",
                Phone = "88888888"
            };
            //Act
            var pass = Customer.Validate(c);
            //Assert
            Assert.IsFalse(pass);
        }

        [TestMethod]
        public void EmployeeValidationPass()
        {
            //Arrange
            var e = new EmployeeDTO
            {
                FirstName = "Alpha",
                LastName = "Beta",
                Address = "Gammavej 4",
                City = "Epsilonby",
                Email = "Alphabeta@example.com",
                ZipCode = "1234",
                Phone = "88888888",
                Title = "BobskiBuilder"
            };
            //Act
            var pass = Employee.Validate(e);
            //Assert
            Assert.IsTrue(pass);
        }

        [TestMethod]
        public void EmployeeValidationNullFail()
        {
            //Arrange
            var e = new EmployeeDTO
            {
                FirstName = "Alpha",
                LastName = "Beta",
                Address = null,
                City = "Epsilonby",
                Email = "Alphabeta@example.com",
                ZipCode = "1234",
                Phone = "88888888",
                Title = "BobskiBuilder"
            };
            //Act
            var pass = Employee.Validate(e);
            //Assert
            Assert.IsFalse(pass);
        }

        [TestMethod]
        public void EmployeeValidationEmptyStringFail()
        {
            //Arrange
            var e = new EmployeeDTO
            {
                FirstName = "Alpha",
                LastName = "Beta",
                Address = "",
                City = "Epsilonby",
                Email = "Alphabeta@example.com",
                ZipCode = "1234",
                Phone = "88888888",
                Title = "BobskiBuilder"
            };
            //Act
            var pass = Employee.Validate(e);
            //Assert
            Assert.IsFalse(pass);
        }

        [TestMethod]
        public void UserValidationPass()
        {
            //Arrange
            var u = new UserDTO
            {
                Username = "Alpha",
                Employee = new EmployeeDTO
                {
                    FirstName = "Alpha",
                    LastName = "Beta",
                    Address = "Gammavej 4",
                    City = "Epsilonby",
                    Email = "Alphabeta@example.com",
                    ZipCode = "1234",
                    Phone = "88888888",
                    Title = "BobskiBuilder"
                },
                AccountType = UserRoles.Employee
            };
            //Act
            var pass = User.Validate(u);
            //Assert
            Assert.IsTrue(pass);
        }

        [TestMethod]
        public void UserValidationNullFail()
        {
            //Arrange
            var u = new UserDTO
            {
                Username = null,
                AccountType = UserRoles.Employee,
                Employee = new EmployeeDTO
                {
                    FirstName = "Alpha",
                    LastName = "Beta",
                    Address = "Gammavej 4",
                    City = "Epsilonby",
                    Email = "Alphabeta@example.com",
                    ZipCode = "1234",
                    Phone = "88888888",
                    Title = "BobskiBuilder"
                }
            };
            //Act
            var pass = User.Validate(u);
            //Assert
            Assert.IsFalse(pass);
        }

        [TestMethod]
        public void UserValidationEmptyStringFail()
        {
            //Arrange
            var u = new UserDTO
            {
                Username = "",
                AccountType = UserRoles.Employee,
                Employee = new EmployeeDTO
                {
                    FirstName = "Alpha",
                    LastName = "Beta",
                    Address = "Gammavej 4",
                    City = "Epsilonby",
                    Email = "Alphabeta@example.com",
                    ZipCode = "1234",
                    Phone = "88888888",
                    Title = "BobskiBuilder"
                }
            };
            //Act
            var pass = User.Validate(u);
            //Assert
            Assert.IsFalse(pass);
        }

        //[TestMethod]
        //public void UservalidationWrongTypeFail() //TODO
        //{

        //}
    }
}