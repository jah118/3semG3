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
        private readonly IReservationService _reservationService;

        public BookingServicesTests(IReservationService reservationService)
        {
            _reservationService = reservationService;
        }


        [TestMethod]
        public void CreateAsyncTest()
        {
            var reservation = new ReservationDTO(1,
                new CustomerDTO(),
                DateTime.Now,
                4,
                false,
                "TEST",
                new List<RestaurantTablesDTO>());

            //Act
            var response = _reservationService.CreateAsync(reservation).Result;

            //Assert
            //Assert.AreEqual(response.StatusCode, HttpStatusCode.OK);
            Assert.IsTrue(response.StatusCode == HttpStatusCode.OK);
        }
    }
}