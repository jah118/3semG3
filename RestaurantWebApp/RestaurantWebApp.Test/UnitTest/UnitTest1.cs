using System;
using System.Text;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using RestSharp;
using System.Configuration;
using Newtonsoft.Json;
using System.Net;
using DataAccess.DataTransferObjects;
using System.Runtime.CompilerServices;

namespace RestaurantWebApp.Test.UnitTest
{
    /// <summary>
    /// Summary description for UnitTest1
    /// </summary>
    [TestClass]
    public class UnitTest1

    {
        public UnitTest1()
        {
            //
            // TODO: Add constructor logic here
            //
        }
        string constring = "https://ptsv2.com/t/axmts-1604922634";
        [TestMethod]
        public void TestPostBookTable()
        {
            //Arrange
            //var client = new RestClient("https://localhost:44349/api/Booking/Create");
            var client = new RestClient(constring); 
            
                var reservation = new ReservationDTO(
                    DateTime.Now,
                    new CustomerDTO(),
                    DateTime.Now,
                    4,
                    false,
                    "TEST",
                    new List<ReservationsTablesDTO>());

            string json = JsonConvert.SerializeObject(reservation);
            //ACT
            var request = new RestRequest("/post", Method.POST);
            // var request = new RestRequest("/Booking/Create", Method.POST);
            // request.AddJsonBody(reservation);
            request.AddJsonBody(json);
            var response = client.Execute(request);


            //Assert
            Assert.AreEqual(response.StatusCode, HttpStatusCode.OK);
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
        #endregion

        [TestMethod]
        public void TestMethod1()
        {
            //
            // TODO: Add test logic here
            //
        }
    }
}
