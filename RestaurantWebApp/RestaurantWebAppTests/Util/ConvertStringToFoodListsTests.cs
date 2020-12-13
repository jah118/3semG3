using Microsoft.VisualStudio.TestTools.UnitTesting;
using RestaurantWebApp.DataTransferObject;
using System.Collections.Generic;

namespace RestaurantWebApp.Util.Tests
{
    [TestClass]
    public class ConvertStringToFoodListsTests
    {
        [TestMethod]
        public void ListOfFoodsIdStringsToFoodListTest_withValidInput()
        {
            var stringFoodId1 = "1";
            var stringFoodId2 = "2";
            var stringFoodId3 = "3";
            var stringFoodId4 = "4";
            var listOfFoodIdStrings = new List<string>();
            listOfFoodIdStrings.Add(stringFoodId1);
            listOfFoodIdStrings.Add(stringFoodId2);
            listOfFoodIdStrings.Add(stringFoodId3);
            listOfFoodIdStrings.Add(stringFoodId4);

            var CompleteFoodInList = new List<FoodDTO>
            {
                new FoodDTO
                {
                    Description = "Some Test Description, for FoodDTO1",
                    FoodCategoryName = "Mad",
                    Id = 1,
                    Name = "Oats Large Flake",
                    Price = 218
                },
                new FoodDTO
                {
                    Description = "Some Test Description, for FoodDTO2",
                    FoodCategoryName = "Drikkevare",
                    Id = 2,
                    Name = "Oil - Food, Lacquer Spray",
                    Price = 202
                },
                new FoodDTO
                {
                    Description = "Some Test Description, for FoodDTO2",
                    FoodCategoryName = "Mad",
                    Id = 3,
                    Name = "Turkey Leg With Drum And Thigh",
                    Price = 179
                },
                new FoodDTO
                {
                    Description = "Some Test Description, for FoodDTO2",
                    FoodCategoryName = "Drikkevare",
                    Id = 4,
                    Name = "Ecolab - Lime - A - Way 4/4 L",
                    Price = 112
                }
            };

            var con = ConvertStringToFoodLists.ListOfFoodsIdStringsToFoodList(listOfFoodIdStrings, CompleteFoodInList);


        }
    }
}