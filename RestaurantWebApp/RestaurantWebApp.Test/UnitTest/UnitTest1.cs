using Microsoft.VisualStudio.TestTools.UnitTesting;
using RestaurantWebApp.DataTransferObject;
using RestaurantWebApp.Service.Interfaces;
using System;
using System.Collections.Generic;

namespace RestaurantWebApp.Test.UnitTest
{
    /// <summary>
    /// Summary description for UnitTest1
    /// </summary>
    [TestClass]
    public class UnitTest1

    {
        private readonly IBookingService _bookingService;
        private readonly ITableService _tableService;

        public UnitTest1(IBookingService bookingService, ITableService tableService)
        {
            _bookingService = bookingService;
            _tableService = tableService;
        }

        private string constring = "https://ptsv2.com/t/axmts-1604922634";

        [TestMethod]
        public void TestPostBookTableAsync()
        {
            //Arrange
            //var client = new RestClient("https://localhost:44349/api/Booking/Create");
            // var client = new RestClient(constring);

            //string json = JsonConvert.SerializeObject(reservation);
            ////ACT
            //var request = new RestRequest("/post", Method.POST);
            //// var request = new RestRequest("/Booking/Create", Method.POST);
            //// request.AddJsonBody(reservation);
            //request.AddJsonBody(json);
            //var response = client.Execute(request);

            //Arrange

            var reservation = new ReservationDTO(
                DateTime.Now,
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
            Assert.IsTrue(response);
        }

        /// <summary>
        ///Gets or sets the test context which provides
        ///information about and functionality for the current test run.
        ///</summary>

        #region Additional test attributes

        //
        // You can use the following additional attributes as you write your tests:
        //
        // Use ClassInitialize to run code before running the first test in the class
        // [ClassInitialize()]
        // public static void MyClassInitialize(TestContext testContext) { }
        //
        // Use ClassCleanup to run code after all tests in a class have run
        // [ClassCleanup()]
        // public static void MyClassCleanup() { }
        //
        // Use TestInitialize to run code before running each test
        // [TestInitialize()]
        // public void MyTestInitialize() { }
        //
        // Use TestCleanup to run code after each test has run
        // [TestCleanup()]
        // public void MyTestCleanup() { }
        //

        #endregion Additional test attributes
    }
}