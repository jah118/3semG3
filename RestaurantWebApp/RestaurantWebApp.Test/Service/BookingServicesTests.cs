using System;
using System.Collections.Generic;
using System.Net;
using System.Runtime.InteropServices;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using RestaurantWebApp.DataTransferObject;
using RestaurantWebApp.Service.Interfaces;


namespace RestaurantWebApp.Test.Service
{
    [TestClass]
    public class BookingServicesTests
    {
        private readonly IBookingService _bookingService;

        public BookingServicesTests(IBookingService bookingService)
        {
            _bookingService = bookingService;
        }


        [TestMethod]
        public void CreateAsyncTest()
        {
            var reservation = new ReservationDTO(
                new CustomerDTO(),
                DateTime.Now,
                4,
                false,
                "TEST",
                new List<RestaurantTablesDTO>());

            //Act
            var response = _bookingService.CreateAsync(reservation).Result;

            //Assert
            //Assert.AreEqual(response.StatusCode, HttpStatusCode.OK);
            Assert.IsTrue(response.StatusCode == HttpStatusCode.OK);
        }
    }
}