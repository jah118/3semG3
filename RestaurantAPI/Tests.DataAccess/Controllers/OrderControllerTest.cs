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
    public class OrderControllerTest
    {
        [TestMethod]
        public void GetByIDTest_Succes()

        {
            //Arrange
            var mock = new Mock<IOrderRepository>();
            var order = new OrderDTO

            {
                OrderNo = 50,
                ReservationID = 49,
                EmployeeID = 80,
                OrderDate = DateTime.Now,
                PaymentCondition = "Betalt",
                OrderLines = new List<OrderLineDTO>()
            };
            mock.Setup(x => x.GetById(It.IsAny<int>())).Returns(order);
            var controller = new OrderController(mock.Object);
            //Act
            var o = mock.Object.GetById(50);
            var result = controller.Get(50);
            var okResult = result as OkObjectResult;
            //Assert
            Assert.IsNotNull(o);
            Assert.AreEqual(order.OrderNo, o.OrderNo);
            Assert.IsNotNull(result);
            Assert.AreEqual(okResult.StatusCode, (int) HttpStatusCode.OK);
        }

        [TestMethod]
        public void GetByIDTest_Failed()
        {
            //Arrange
            var mock = new Mock<IOrderRepository>();
            var order = new OrderDTO

            {
                OrderNo = 50,
                ReservationID = 49,
                EmployeeID = 80,
                OrderDate = DateTime.Now,
                PaymentCondition = "Betalt",
                OrderLines = new List<OrderLineDTO>()
            };
            mock.Setup(x => x.GetById(50)).Returns(order);
            var controller = new OrderController(mock.Object);
            //Act
            var o = mock.Object.GetById(55);
            var result = controller.Get(55);
            var okResult = result as NotFoundObjectResult;
            //Assert
            Assert.IsNull(o);
            Assert.AreEqual(okResult.StatusCode, (int) HttpStatusCode.NotFound);
        }


        [TestMethod]
        public void CreateOrderTest_Succes()
        {
            //Arrange
            var mock = new Mock<IOrderRepository>();
            var order = new OrderDTO
            {
                OrderNo = 50,
                ReservationID = 49,
                EmployeeID = 80,
                OrderDate = DateTime.Now,
                PaymentCondition = "Betalt",
                OrderLines = new List<OrderLineDTO>()
            };

            mock.Setup(x => x.Create(order, true)).Returns(order);
            var controller = new OrderController(mock.Object);
            //Act
            var o = mock.Object.Create(order);
            var result = controller.Post(order);
            var okResult = result as OkObjectResult;
            //Assert
            Assert.IsNotNull(o);
            Assert.AreEqual(order.OrderNo, o.OrderNo);
            Assert.AreEqual(order.ReservationID, o.ReservationID);
            Assert.AreEqual(order.EmployeeID, o.EmployeeID);
            Assert.AreEqual(order.OrderDate, o.OrderDate);
            Assert.AreEqual(order.PaymentCondition, o.PaymentCondition);
            Assert.AreEqual(order.OrderLines, o.OrderLines);
            Assert.IsNotNull(result);
            Assert.AreEqual(okResult.StatusCode, (int) HttpStatusCode.OK);
        }

        [TestMethod]
        public void CreateOrderTest_Failed()
        {
            //Arrange
            OrderDTO o1 = null;
            var mock = new Mock<IOrderRepository>();
            var order = new OrderDTO
            {
                OrderNo = 0,
                ReservationID = 49,
                EmployeeID = 80,
                OrderDate = DateTime.Now,
                PaymentCondition = "Betalt",
                OrderLines = new List<OrderLineDTO>()
            };

            mock.Setup(x => x.Create(order, true)).Returns(o1);
            var controller = new OrderController(mock.Object);
            //Act
            var o = mock.Object.Create(order);
            var result = controller.Post(order);
            var okResult = result as ConflictObjectResult;

            //Assert
            Assert.AreEqual(okResult.StatusCode, (int) HttpStatusCode.Conflict);
        }

        [TestMethod]
        public void GetAllOrdersTest_Succes()
        {
            //Arrange
            var mock = new Mock<IOrderRepository>();
            var order = new OrderDTO
            {
                OrderNo = 50,
                ReservationID = 49,
                EmployeeID = 80,
                OrderDate = DateTime.Now,
                PaymentCondition = "Betalt",
                OrderLines = new List<OrderLineDTO>()
            };
            var order1 = new OrderDTO
            {
                OrderNo = 49,
                ReservationID = 20,
                EmployeeID = 99,
                PaymentCondition = "Begyndt",
                OrderLines = new List<OrderLineDTO>()
            };

            IList<OrderDTO> orders = new List<OrderDTO>();

            orders.Add(order);
            orders.Add(order1);


            mock.Setup(x => x.GetAll()).Returns(orders);
            var controller = new OrderController(mock.Object);
            //Act
            var o = mock.Object.GetAll();
            var result = controller.Get();
            var okResult = result as OkObjectResult;
            //Arrange
            Assert.IsNotNull(o);

            Assert.IsTrue(o.Count() > 1);
            Assert.AreEqual(order.OrderNo, o.ElementAt(0).OrderNo);
            Assert.AreEqual(order1.OrderNo, o.ElementAt(1).OrderNo);

            Assert.IsNotNull(result);
            Assert.AreEqual(okResult.StatusCode, (int) HttpStatusCode.OK);
        }
    }
}