﻿using DataAccess;
using DataAccess.DataTransferObjects;
using DataAccess.Models;
using DataAccess.Repositories;
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
    public class OrderControllerTest
    {

        [TestMethod]
        public void PostValidOrder()
        {
            //var controller = new Mock<RestaurantContext>();
            //var mockBehavior = new MockBehavior<>            
            //var mockContext = new MockRepository(mockBehavior);
            //OrderDTO order = new OrderDTO()
            //{
            //    Employee = new EmployeeDTO() { Id = 2 },
            //    OrderDate = DateTime.Now.AddDays(-1),
            //    ReservationID = 1,
            //    PaymentCondition = "Begyndt",
            //    Foods = new List<FoodDTO>() {
            //        new FoodDTO()
            //        {
            //            Id = 2,
            //            Name = "Oats Large Flake",
            //            Description = "Nunc nisl. Duis bibendum, felis sed interdum venenatis, turpis enim blandit mi, in porttitor pede justo eu massa. Donec dapibus. Duis at velit eu est congue elementum.",
            //            Price = 200,
            //            Quantity = 2,
            //            FoodCategoryName = "mad"
            //        }
            //    }
            //};
            //var result = mockContext.SetupAdd(_=>_.Order.Add(order);
            //Assert.IsTrue(1 == 1);
        }

    }
}