using DataAccess.Repositories.Interfaces;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using RestaurantAPI.Controllers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataAccess.DataTransferObjects;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace RestaurantAPI.Controllers.Tests
{
    [TestClass()]
    public class TableControllerTests
    {

        [TestMethod()]
        public void GetAll()
        {
            var mock = new Mock<ITableRepository>();
            RestaurantTablesDTO table1 = new RestaurantTablesDTO
            {
                Id = 8,
                NoOfSeats = 3,
                TableNumber = 8

            };
            RestaurantTablesDTO table2 = new RestaurantTablesDTO
            {
                Id = 19,
                NoOfSeats = 3,
                TableNumber = 7

            };


            IList<RestaurantTablesDTO> tables = new List<RestaurantTablesDTO>();
            tables.Add(table1);
            tables.Add(table2);


            mock.Setup(x => x.GetAll()).Returns(tables);
            var controller = new TableController(mock.Object);
            var t = mock.Object.GetAll();
            var result = controller.Get();
            var okResult = result as OkObjectResult;

            Assert.IsNotNull(t);
            Assert.IsTrue(t.Count() > 1);
            Assert.AreEqual(table1.Id, t.ElementAt(0).Id);
            Assert.AreEqual(table2.Id, t.ElementAt(1).Id);

            Assert.IsNotNull(result);
            Assert.AreEqual(okResult.StatusCode, (int)HttpStatusCode.OK);


        }

        [TestMethod()]
        public void GetOpenTablesTest()
        {
            var mock = new Mock<ITableRepository>();
            RestaurantTablesDTO table1 = new RestaurantTablesDTO
            {
                Id = 8,
                NoOfSeats = 3,
                TableNumber = 8

            };
            RestaurantTablesDTO table2 = new RestaurantTablesDTO
            {
                Id = 19,
                NoOfSeats = 3,
                TableNumber = 7

            };


            List<RestaurantTablesDTO> tables = new List<RestaurantTablesDTO>();
            tables.Add(table1);
            tables.Add(table2);
            DateTime date = new DateTime(2020, 12, 12, 18, 00, 00);

            mock.Setup(x => x.GetOpenTablesByDateAndTime(date)).Returns(tables);
            var controller = new TableController(mock.Object);
            var t = mock.Object.GetOpenTablesByDateAndTime(date);

            var result = controller.GetOpenTables(date);
            var okResult = result as OkObjectResult;

            Assert.IsNotNull(t);
            Assert.IsTrue(t.Count() > 1);
            Assert.AreEqual(table1.Id, t.ElementAt(0).Id);
            Assert.AreEqual(table2.Id, t.ElementAt(1).Id);

            Assert.IsNotNull(result);
            Assert.AreEqual(okResult.StatusCode, (int)HttpStatusCode.OK);

        }




        [TestMethod()]
        public void GetBydate()

        {
            var mock = new Mock<ITableRepository>();

            var timePairList = new List<AvailableTimesDTO.TableTimes.TimePair>();
            var t1 = new AvailableTimesDTO.TableTimes.TimePair
            {
                Start = new DateTime(2020, 03, 10, 12, 00, 00),
                End = new DateTime(2020, 03, 10, 22, 00, 00)
            };
            timePairList.Add(t1);

            AvailableTimesDTO available = new AvailableTimesDTO()
            {
                AvailabilityDate = new DateTime(2020, 03, 10),
                TableOpenings = new List<AvailableTimesDTO.TableTimes>
                {
                    new AvailableTimesDTO.TableTimes
                    {
                        Table = new RestaurantTablesDTO
                        {
                            Id = 8,
                            NoOfSeats = 3,
                            TableNumber = 8

                        },
                        Openings = timePairList
                    }
                }
            };

            
            DateTime date = new DateTime(2020, 12, 12, 18, 00, 00);



            mock.Setup(x => x.GetReservationTimeByDate(date)).Returns(available);
            var controller = new TableController(mock.Object);
            var t = mock.Object.GetReservationTimeByDate(date);

            var result = controller.Get(date);
            var okResult = result as OkObjectResult;

            Assert.IsNotNull(t);
            Assert.IsTrue(t.TableOpenings.Count() > 0);
        
            Assert.IsNotNull(result);
            Assert.AreEqual(okResult.StatusCode, (int)HttpStatusCode.OK);

        }
    }
}