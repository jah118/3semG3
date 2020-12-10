using Microsoft.VisualStudio.TestTools.UnitTesting;
using RestaurantWebApp.DataTransferObject;

namespace RestaurantWebApp.Test.Util
{
    [TestClass()]
    public class ConvertStringToFoodListsTests
    {
        [TestMethod()]
        public void ListOfFoodsIdStringsToFoodListTest()
        {

            const string stringOffoods = "1,2,3,5,7";
            var food1 = new FoodDTO(1);
            var food2 = new FoodDTO(2);
            var food3 = new FoodDTO(3);
            var food4 = new FoodDTO(4);
            var food5 = new FoodDTO(5);


            Assert.Fail();
        }
    }
}