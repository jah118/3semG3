using System;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using RestaurantWebApp.DataTransferObject;
using RestaurantWebApp.Util;

namespace RestaurantWebApp.Test.Util
{
    [TestClass]
    public class FormatStringToTablesTests
    {
        [TestMethod]
        public void StringOfIdToTablesTestValid()
        {
            //Arrange
            const string stringOfTables = "1,2,3,5,7";

            var firstTable = new RestaurantTablesDTO(1, 0, 0);
            var thirdTable = new RestaurantTablesDTO(3, 0, 0);
            var lastTable = new RestaurantTablesDTO(7, 0, 0);

            //Act
            var tables = FormatStringToTables.StringOfIdToTables(stringOfTables);
            var restaurantTables = tables.ToList();

            //Assert
            Assert.IsNotNull(tables);
            Assert.AreEqual(5, restaurantTables.Count());

            Assert.AreEqual(firstTable.Id, restaurantTables.ElementAt(0).Id);
            Assert.AreEqual(thirdTable.Id, restaurantTables.ElementAt(2).Id);
            Assert.AreEqual(lastTable.Id, restaurantTables.ElementAt(4).Id);
        }

        [TestMethod]
        public void StringOfIdToTablesTestInvalidStringAndTrowsException()
        {
            //Arrange
            const string stringOfTables = "1, ,3,5,7";

            //Act /
            Assert.ThrowsException<FormatException>(() => FormatStringToTables.StringOfIdToTables(stringOfTables));
        }

        [TestMethod]
        public void StringOfIdToTablesTestInvalidEmptyStringTrowsException()
        {
            //Arrange
            const string stringOfTables = "   ";

            //Act /
            Assert.ThrowsException<FormatException>(() => FormatStringToTables.StringOfIdToTables(stringOfTables));
        }
    }
}