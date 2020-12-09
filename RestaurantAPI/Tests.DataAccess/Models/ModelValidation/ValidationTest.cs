using DataAccess.DataTransferObjects;
using DataAccess.Models;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Tests.DataAccess.Models.ModelValidation
{
    [TestClass, TestCategory("Validation")]
    public class ValidationTest
    {
        [TestMethod]
        public void CustomerValidationPass()
        {
            //Arrange
            CustomerDTO c = new CustomerDTO()
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
            bool pass = Customer.Validate(c);
            //Assert
            Assert.IsTrue(pass);
        }

        [TestMethod]
        public void CustomerValidationWithNullFail()
        {
            //Arrange
            CustomerDTO c = new CustomerDTO()
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
            bool pass = Customer.Validate(c);
            //Assert
            Assert.IsFalse(pass);
        }

        [TestMethod]
        public void CustomerValidationWithEmptyStringFail()
        {
            //Arrange
            CustomerDTO c = new CustomerDTO()
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
            bool pass = Customer.Validate(c);
            //Assert
            Assert.IsFalse(pass);
        }

        [TestMethod]
        public void EmployeeValidationPass()
        {
            //Arrange
            EmployeeDTO e = new EmployeeDTO()
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
            bool pass = Employee.Validate(e);
            //Assert
            Assert.IsTrue(pass);
        }

        [TestMethod]
        public void EmployeeValidationNullFail()
        {
            //Arrange
            EmployeeDTO e = new EmployeeDTO()
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
            bool pass = Employee.Validate(e);
            //Assert
            Assert.IsFalse(pass);
        }

        [TestMethod]
        public void EmployeeValidationEmptyStringFail()
        {
            //Arrange
            EmployeeDTO e = new EmployeeDTO()
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
            bool pass = Employee.Validate(e);
            //Assert
            Assert.IsFalse(pass);
        }

        [TestMethod]
        public void UserValidationPass()
        {
            //Arrange
            UserDTO u = new UserDTO()
            {
                Username = "Alpha",
               Employee = new EmployeeDTO()
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
            bool pass = User.Validate(u);
            //Assert
            Assert.IsTrue(pass);
        }

        [TestMethod]
        public void UserValidationNullFail()
        {
            //Arrange
            UserDTO u = new UserDTO()
            {
                Username = null,
                AccountType = UserRoles.Employee,
                Employee = new EmployeeDTO()
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
            bool pass = User.Validate(u);
            //Assert
            Assert.IsFalse(pass);
        }

        [TestMethod]
        public void UserValidationEmptyStringFail()
        {
            //Arrange
            UserDTO u = new UserDTO()
            {
                Username = "",
                AccountType = UserRoles.Employee,
                Employee = new EmployeeDTO()
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
            bool pass = User.Validate(u);
            //Assert
            Assert.IsFalse(pass);
        }

        [TestMethod]
        public void UservalidationWrongTypeFail()
        {

        }
    }
}
