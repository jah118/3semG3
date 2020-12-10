using Autofac;
using Autofac.Extras.Moq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using RestaurantWebApp.Controllers;
using RestaurantWebApp.DataTransferObject;
using RestaurantWebApp.Service;
using RestaurantWebApp.Service.Interfaces;
using RichardSzalay.MockHttp;
using System;
using System.Collections.Generic;
using System.Text;
using System.Web.Mvc;

namespace RestaurantWebApp.Test.Service
{
    /// <summary>
    /// Summary description for BookingControllerTest
    /// </summary>
    [TestClass]
    public class BookingControllerTest
    {
        //public BookingControllerTest()
        //{

        //}

        //TODO testmetode mangler at orderfood er færdig
        //[TestMethod]
        //public void OrderFoodSuccesTest()
        //{

        //    using (var mock = AutoMock.GetLoose())
        //    {
        //        var orderFood = GetFoods();

        //        // mock.Mock<OrderDTO>().Setup(x => x.); 


        //    }

        //}
        //private List<FoodDTO> GetFoods()
        //{
        //    List<FoodDTO> foods = new List<FoodDTO>
        //    {
        //     new FoodDTO()
        //      {
                 
        //            Name = "Bøf",
        //            Description = "god udskæring",
        //            FoodCategoryName = "mad",
        //            Price = 189.9


        //        },
        //    new FoodDTO
        //    {
        //        Name = "Vin",
        //        Description = "Frisk ",
        //        FoodCategoryName = "drikkevarer",
        //        Price = 300.88,


        //    },
        //    new FoodDTO
        //    {
        //        Name = "Salat",
        //        Description = "Nyskåret og frisk",
        //        FoodCategoryName = "mad",
        //        Price = 49.50,


        //    }

        //};
        //    return foods;
        //}

        [TestMethod]
        public void CreateReservationSuccesTest()
        {
            //Arrange
            //Action<ContainerBuilder> beforeBuild = delegate (ContainerBuilder cb)
            // {
            //     cb.Build();
            // };

            using (var mock = AutoMock.GetLoose())
            {
                //mock.Mock<IReservationService>().Setup(x => x.Create(ReservationService()))
                //    .Returns(() => new ReservationService());

                //var mockReservation = new Mock<ReservationDTO>();
                //mockReservation.SetupAllProperties();
                //mockReservation.SetupGet(p => p.NoOfPeople).Returns(5);
                //mockReservation.SetupGet(p => p.Note).Returns("Vigtig Info");
                //mockReservation.SetupGet(p => p.ReservationTime).Returns(new DateTime(12, 12, 2020, 18, 30, 00));
                //mockReservation.SetupGet(p => p.Customer).Returns(getCustomer());
                //mockReservation.SetupGet(p => p.Deposit).Returns(false);
                //mockReservation.SetupGet(p => p.OrderingFood).Returns(false);
                //mockReservation.SetupGet(p => p.Tables).Returns(new List<RestaurantTablesDTO> { new RestaurantTablesDTO(97, 2, 97), new RestaurantTablesDTO(98, 4, 98) });
                //mockReservation.SetupGet<IList<ReservationDTO>>(p => p.TimeSlots()).Returns<IEnumerable<ReservationDTO> ({});

                //mockReservation.Object.NoOfPeople = 4;
                //mockReservation.Object.Note = "Info";
                //mockReservation.Object.OrderingFood = false;
                //mockReservation.Object.ReservationTime = new DateTime(2020, 12, 12, 18, 30, 00);
                //mockReservation.Object.Customer = getCustomer();
                //mockReservation.Object.Tables = new List<RestaurantTablesDTO> { new RestaurantTablesDTO(97, 2, 97), new RestaurantTablesDTO(98, 4, 98) };


                //Action act = () =>
                //{
                //   var reservation = new ReservationDTO (mockReservation.Object);
                //   Assert.AreEqual("false", reservation.OrderingFood);
                //   Assert.AreEqual("4", reservation.NoOfPeople);
                //   Assert.AreEqual("Info", reservation.Note);
                //   Assert.AreEqual("new DateTime(12, 12, 2020, 18, 30, 00)", reservation.ReservationTime);
                //   Assert.AreEqual("getCustomer()", reservation.Customer);
                //   Assert.AreEqual("new List<RestaurantTablesDTO> { new RestaurantTablesDTO(97, 2, 97), new RestaurantTablesDTO(98, 4, 98) })", reservation.Tables);

                //};



                var mockHttp = new MockHttpMessageHandler();
                var mockBase = new Mock<BookingController>() { CallBase = true };

                mockHttp.When("https://localhost:44386/api").Respond("application/json", "{reservation");

                

                //mockTables.Setup(x => x.CreateAsync(It.IsAny<ReservationDTO>())).Returns(() => new id());

               var reservartionService = new ReservationDTO();
               var cls = mock.Create<ReservationService>();
                
                //act
                cls.Create(reservartionService);

                //Assert

                mock.Mock<IReservationService>().Verify(x => x.CreateAsync(reservartionService), Times.Exactly(1));
            }

        }

        private CustomerDTO getCustomer()
        {
            CustomerDTO c = new CustomerDTO
            {
                FirstName = "Brian",
                LastName = "Larsen",
                Phone = "99889900",
                Email = "Brian@Larsen.dk",
                Address = "Larsenvej 12",
                ZipCode = "9000",
                City = "Aalborg"
            };
            return c;

        }


        private TestContext testContextInstance;

        /// <summary>
        ///Gets or sets the test context which provides
        ///information about and functionality for the current test run.
        ///</summary>
        public TestContext TestContext
        {
            get
            {
                return testContextInstance;
            }
            set
            {
                testContextInstance = value;
            }
        }

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
