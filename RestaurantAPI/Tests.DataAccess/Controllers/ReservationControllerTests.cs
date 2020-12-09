using Microsoft.VisualStudio.TestTools.UnitTesting;
using RestaurantAPI.Controllers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataAccess.DataTransferObjects;
using DataAccess.Repositories.Interfaces;
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
            var res= new ReservationDTO(
                    41,
                    DateTime.Now,
                    new CustomerDTO(),
                    DateTime.Now,
                    5,
                    false,
                    "TEST",
                    new List<RestaurantTablesDTO> { new RestaurantTablesDTO(), new RestaurantTablesDTO() }

                    );
           
          mock.Setup(x => x.GetById(It.IsAny<int>())).Returns(res);
          var cur = mock.Object.GetById(41);
            Assert.IsNotNull(cur);
            Assert.AreEqual(res.Id, cur.Id);
        }
    }
}
