using Microsoft.VisualStudio.TestTools.UnitTesting;
using RestaurantAPI.Controllers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using DataAccess.DataTransferObjects;
using DataAccess.Repositories;
using DataAccess.Repositories.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Moq;

namespace RestaurantAPI.Controllers.Tests
{
    [TestClass]
    public class CustomerControllerTest
    {
        [TestMethod]
        public void GetByIDTest()
        {
            // using (var mock = AutoMock.GetLoose())
            var repository = new Mock<ICustomerRepository>();

            CustomerDTO customer = new CustomerDTO
            {
                Id = 10,
                FirstName = "Søren",
                LastName = "Larsen",
                Address = "Larsenvej 3",
                Email = "Søren@larsen.dk",
                Phone = "90876653",
                ZipCode = "9000",
                City = "Aalborg",

            };


            repository.Setup(x => x.GetById(It.IsAny<int>())).Returns(customer);
            var controller = new CustomerController(repository.Object);
            var c = repository.Object.GetById(10);
            var result = controller.Get(10);
            var okResult = result as OkObjectResult;

            Assert.IsNotNull(c);
            Assert.AreEqual(customer.Id, c.Id);
            Assert.IsNotNull(result);
            Assert.AreEqual(okResult.StatusCode, (int)HttpStatusCode.OK);
        }

    }
}
