using DataAccess;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RestaurantAPI.Controllers.Tests
{
    [TestClass]
    public class ReservationRepositoryTest
    {
        
        [TestMethod]
        public void PostValidReservation()
        {
            

        }

        [TestMethod, TestCategory("Unit")]
        public void GetAllReservations()
        {
            var mockContext = new Mock<RestaurantContext>();
            //mockContext.Setup(p => p.);

            Assert.IsTrue(true);
        }
    }
}
