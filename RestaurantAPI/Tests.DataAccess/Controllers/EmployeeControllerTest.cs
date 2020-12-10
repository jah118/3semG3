using Autofac.Extras.Moq;
using DataAccess;
using DataAccess.DataTransferObjects;
using DataAccess.Repositories.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using RestaurantAPI.Controllers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace Tests.DataAccess
{
    [TestClass]
   public class EmployeeControllerTest
    {

        [TestMethod]
        public void GetEmployeeFromDatababaseById()
        {

            var repository = new Mock<IEmployeeRepository>();

            var employee = new EmployeeDTO
                {
                    Id = 12,
                    Title =  "Tjener",
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
                var c = repository.Object.GetById(10);
                var result = controller.Get(10);
                var okResult = result as OkObjectResult;

                Assert.IsNotNull(c);
                Assert.AreEqual(employee.Id, c.Id);
                Assert.IsNotNull(result);
                Assert.AreEqual(okResult.StatusCode, (int)HttpStatusCode.OK);
            }
        
    }
}
