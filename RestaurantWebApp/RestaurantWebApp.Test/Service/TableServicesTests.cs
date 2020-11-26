using Microsoft.VisualStudio.TestTools.UnitTesting;
using RestaurantWebApp.Service.Interfaces;

namespace RestaurantWebApp.Test.Service
{
    [TestClass()]
    public class TableServicesTests
    {
        private readonly ITableService _tableService;

        public TableServicesTests(ITableService tableService)
        {
            _tableService = tableService;
        }
        [TestMethod()]
        public void GetAllAsyncTest()
        {
            Assert.Fail();
        }
    }
}