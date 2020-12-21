using System.Net;
using DataAccess.DataTransferObjects;
using DataAccess.Repositories.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;

namespace RestaurantAPI.Controllers.Tests
{
    [TestClass]
    public class EmployeeControllerTest
    {
        [TestMethod]
        public void GetEmployeeFromDatababaseById()
        {
            //Arrange
            var repository = new Mock<IEmployeeRepository>();

            var employee = new EmployeeDTO
            {
                Id = 12,
                Title = "Tjener",
                Phone = "88775522",
                Email = "ABC@BCA.dk",
                FirstName = "Anders",
                LastName = "Svendsen",
                Address = "Svendsensvej 22",
                ZipCode = "9000",
                City = "Aalborg"
            };

            repository.Setup(x => x.GetById(It.IsAny<int>())).Returns(employee);
            var controller = new EmployeeController(repository.Object);
            //Act
            var c = repository.Object.GetById(10);
            var result = controller.Get(10);
            var okResult = result as OkObjectResult;
            //Assert
            Assert.IsNotNull(c);
            Assert.AreEqual(employee.Id, c.Id);
            Assert.IsNotNull(result);
            Assert.AreEqual(okResult.StatusCode, (int) HttpStatusCode.OK);
        }
    }
}