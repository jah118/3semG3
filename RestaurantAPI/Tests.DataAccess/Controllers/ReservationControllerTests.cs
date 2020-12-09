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
    [TestClass()]
    public class ReservationControllerTests
    {
       



        [TestMethod()]
        public void GetByIDTest()
        {
            var mock = new Mock<IReservationRepository>();
             ReservationDTO obj = new ReservationDTO(
                41,
                DateTime.Now,
                new CustomerDTO(),
                DateTime.Now,
                5,
                false,
                "TEST",
                new List<RestaurantTablesDTO> { new RestaurantTablesDTO(), new RestaurantTablesDTO() }

            );

            mock.Setup(x => x.GetById(It.IsAny<int>())).Returns(obj);
            var controller = new ReservationController(mock.Object);
            var value = mock.Object.GetById(41);
            var actionResult = controller.Get(41);
            var okResult = actionResult as OkObjectResult;

            Assert.IsNotNull(value);
            Assert.AreEqual(obj.Id, value.Id);
            Assert.IsNotNull(actionResult);
            //Assert.AreEqual(okResult.StatusCode, HttpStatusCode.OK); //TODO fix so type comapre match

        }

    }
}
