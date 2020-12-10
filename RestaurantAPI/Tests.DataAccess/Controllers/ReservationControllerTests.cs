﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using DataAccess.DataTransferObjects;
using DataAccess.Repositories.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;

namespace RestaurantAPI.Controllers.Tests
{
    [TestClass]
    public class ReservationControllerTests
    {
        #region Getter
        [TestMethod]
        public void GetAll_ShouldReturnOK()
        {
            //Arrange
            var mock = new Mock<IReservationRepository>();
            IList<ReservationDTO> objList = new List<ReservationDTO>();
            var obj = new ReservationDTO(
                41,
                DateTime.Now,
                new CustomerDTO(),
                DateTime.Now,
                5,
                false,
                "TEST",
                new List<RestaurantTablesDTO> { new RestaurantTablesDTO(), new RestaurantTablesDTO() }
            );
            var obj2 = new ReservationDTO(
                42,
                DateTime.Now,
                new CustomerDTO(),
                DateTime.Now,
                5,
                false,
                "TEST2",
                new List<RestaurantTablesDTO> { new RestaurantTablesDTO(), new RestaurantTablesDTO() }
            );
            objList.Add(obj);
            objList.Add(obj2);
            mock.Setup(x => x.GetAll()).Returns(objList);
            var controller = new ReservationController(mock.Object);

            // act
            var value = mock.Object.GetAll();
            var actionResult = controller.Get();
            var okResult = actionResult as OkObjectResult;
            var value2 = mock.Object.GetById(42);
            var actionResult2 = controller.Get(42);
            var okResult2 = actionResult as OkObjectResult;

            // assert
            Assert.IsNotNull(value);
            Assert.IsTrue(value.Count() > 1);
            Assert.AreEqual(obj.Id, value.ElementAt(0).Id);
            Assert.AreEqual(obj2.Id, value.ElementAt(1).Id);

            Assert.IsNotNull(actionResult);
            Assert.AreEqual(okResult.StatusCode, (int)HttpStatusCode.OK);
        }

        [TestMethod]
        public void GetByID_WhenIdIsValid_ShouldReturnOK()
        {
            // arrange
            var mock = new Mock<IReservationRepository>();
            var obj = new ReservationDTO(
                41,
                DateTime.Now,
                new CustomerDTO(),
                DateTime.Now,
                5,
                false,
                "TEST",
                new List<RestaurantTablesDTO> { new RestaurantTablesDTO(), new RestaurantTablesDTO() }
            );

            // act
            mock.Setup(x => x.GetById(It.IsAny<int>())).Returns(obj);
            var controller = new ReservationController(mock.Object);
            var value = mock.Object.GetById(41);
            var actionResult = controller.Get(41);
            var okResult = actionResult as OkObjectResult;

            // assert
            Assert.IsNotNull(value);
            Assert.AreEqual(obj.Id, value.Id);
            Assert.IsNotNull(actionResult);
            Assert.AreEqual(okResult.StatusCode, (int)HttpStatusCode.OK); //TODO fix so type comapre match
        }

        [TestMethod]
        public void GetByID_WhenIdIsValid_ShouldReturnNotFound()
        {
            // arrange
            var mock = new Mock<IReservationRepository>();
            var obj = new ReservationDTO(
                40,
                DateTime.Now,
                new CustomerDTO(),
                DateTime.Now,
                5,
                false,
                "TEST",
                new List<RestaurantTablesDTO> { new RestaurantTablesDTO(), new RestaurantTablesDTO() }
            );
            mock.Setup(x => x.GetById(40)).Returns(obj);
            var controller = new ReservationController(mock.Object);

            // act
            var value = mock.Object.GetById(41);
            var actionResult = controller.Get(41);
            var okResult = actionResult as NotFoundObjectResult;

            // assert
            Assert.IsNull(value);
            Assert.AreEqual(okResult.StatusCode, (int)HttpStatusCode.NotFound);
        }


        #endregion

        #region Posts

        [TestMethod]
        public void Create_WhenReservationIsValid_ShouldReturnOK()
        {


            // arrange
            var mock = new Mock<IReservationRepository>();
            var obj = new ReservationDTO(
                41,
                DateTime.Now,
                new CustomerDTO(),
                DateTime.Now,
                5,
                false,
                "TEST",
                new List<RestaurantTablesDTO> { new RestaurantTablesDTO(), new RestaurantTablesDTO() }
            );

            ReservationDTO obj1 = new ReservationDTO(
                0,
                DateTime.Now,
                new CustomerDTO(), 
                DateTime.Now,
                5,
                false,
                "TEST",
                new List<RestaurantTablesDTO> { new RestaurantTablesDTO(), new RestaurantTablesDTO() }
            );

            // act
            mock.Setup(x => x.Create(obj1,true)).Returns(obj);
            var controller = new ReservationController(mock.Object);
            var value = mock.Object.Create(obj1,true);
            var actionResult = controller.Post(obj1);
            var okResult = actionResult as OkObjectResult;


            // assert
            Assert.IsNotNull(value);
            Assert.IsTrue(value.Id == 41);
            Assert.IsNotNull(actionResult);
            Assert.AreEqual(okResult.StatusCode, (int)HttpStatusCode.OK); //TODO fix so type comapre match
        }


        [TestMethod]
        public void Create_WhenReservationIsNotValid_ShouldReturnConflict()
        {

            ReservationDTO test = null;
            // arrange
            var mock = new Mock<IReservationRepository>();
            var obj = new ReservationDTO(
                41,
                DateTime.Now,
                new CustomerDTO(),
                DateTime.Now,
                5,
                false,
                "TEST",
                new List<RestaurantTablesDTO> { new RestaurantTablesDTO(), new RestaurantTablesDTO() }
            );

            ReservationDTO obj1 = new ReservationDTO(
                0,
                DateTime.Now,
                new CustomerDTO(),
                DateTime.Now,
                5,
                false,
                "TEST",
                new List<RestaurantTablesDTO> { new RestaurantTablesDTO(), new RestaurantTablesDTO() }
            );

            // act
            mock.Setup(x => x.Create(obj1, true)).Returns(test);
            var controller = new ReservationController(mock.Object);
            var value = mock.Object.Create(obj1, true);
            var actionResult = controller.Post(obj1);
            var conflictObjectResult = actionResult as ConflictObjectResult;


            // assert
            Assert.AreEqual(conflictObjectResult.StatusCode, (int)HttpStatusCode.Conflict); //TODO fix so type comapre match
        }

        #endregion
    }
}