using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using RestaurantWebApp.DataTransferObject;
using RestaurantWebApp.Util;

namespace RestaurantWebApp.Test.Util
{
    [TestClass]
    public class ConvertStringToFoodListsTests
    {
        [TestMethod]
        public void ListOfFoodsIdStringsToFoodListTest_withValidInput()
        {
            //Arrange
            var stringFoodId1 = "1";
            var stringFoodId2 = "2";
            var stringFoodId3 = "3";
            var stringFoodId4 = "4";
            var listOfFoodIdStrings = new List<string> { stringFoodId1, stringFoodId2, stringFoodId3, stringFoodId4 };

            #region Setup4FoodDto
            var food1 = new FoodDTO
            {
                Description = "Some Test Description, for FoodDTO1",
                FoodCategoryName = "Mad",
                Id = 1,
                Name = "Oats Large Flake",
                Price = 218
            };
            var food2 = new FoodDTO
            {
                Description = "Some Test Description, for FoodDTO2",
                FoodCategoryName = "Drikkevare",
                Id = 2,
                Name = "Oil - Food, Lacquer Spray",
                Price = 202
            };
            var food3 = new FoodDTO
            {
                Description = "Some Test Description, for FoodDTO2",
                FoodCategoryName = "Mad",
                Id = 3,
                Name = "Turkey Leg With Drum And Thigh",
                Price = 179
            };
            var food4 = new FoodDTO
            {
                Description = "Some Test Description, for FoodDTO2",
                FoodCategoryName = "Drikkevare",
                Id = 4,
                Name = "Ecolab - Lime - A - Way 4/4 L",
                Price = 112
            };


            #endregion

            var completeFoodInAList = new List<FoodDTO> { food1, food2, food3, food4 };

            //Act
            var foods = ConvertStringToFoodLists.ListOfFoodsIdStringsToFoodList(listOfFoodIdStrings,
                completeFoodInAList);

            //Assert
            Assert.IsNotNull(foods);
            Assert.AreEqual(4, foods.Count);

            Assert.IsNotNull(foods.ElementAt(0));
            Assert.IsNotNull(foods.ElementAt(1));
            Assert.IsNotNull(foods.ElementAt(2));
            Assert.IsNotNull(foods.ElementAt(3));

            Assert.AreEqual(food1.Id, foods.ElementAt(0).Id);
            Assert.AreEqual(food2.Id, foods.ElementAt(1).Id);
            Assert.AreEqual(food3.Id, foods.ElementAt(2).Id);
            Assert.AreEqual(food4.Id, foods.ElementAt(3).Id);
        }


        [TestMethod]
        public void ListOfFoodsIdStringsToFoodListTest_withBadInput()
        {
            //Arrange
            var stringFoodId1 = "1";
            var stringFoodId2 = "2";
            var stringFoodId3 = "      ";
            var stringFoodId4 = "4";
            var listOfFoodIdStrings = new List<string> { stringFoodId1, stringFoodId2, stringFoodId3, stringFoodId4 };

            #region Setup4FoodDto
            var food1 = new FoodDTO
            {
                Description = "Some Test Description, for FoodDTO1",
                FoodCategoryName = "Mad",
                Id = 1,
                Name = "Oats Large Flake",
                Price = 218
            };
            var food2 = new FoodDTO
            {
                Description = "Some Test Description, for FoodDTO2",
                FoodCategoryName = "Drikkevare",
                Id = 2,
                Name = "Oil - Food, Lacquer Spray",
                Price = 202
            };
            var food3 = new FoodDTO
            {
                Description = "Some Test Description, for FoodDTO2",
                FoodCategoryName = "Mad",
                Id = 3,
                Name = "Turkey Leg With Drum And Thigh",
                Price = 179
            };
            var food4 = new FoodDTO
            {
                Description = "Some Test Description, for FoodDTO2",
                FoodCategoryName = "Drikkevare",
                Id = 4,
                Name = "Ecolab - Lime - A - Way 4/4 L",
                Price = 112
            };


            #endregion

            var completeFoodInAList = new List<FoodDTO> { food1, food2, food3, food4 };

            // Act => Assert
            Assert.ThrowsException<FormatException>(() => ConvertStringToFoodLists
                .ListOfFoodsIdStringsToFoodList(listOfFoodIdStrings, completeFoodInAList));
            
        }

        [TestMethod]
        public void ListOfFoodsIdStringsToFoodListTest_withNoInput()
        {
            //Arrange

            var listOfFoodIdStrings = new List<string>();

            #region Setup4FoodDto
            var food1 = new FoodDTO
            {
                Description = "Some Test Description, for FoodDTO1",
                FoodCategoryName = "Mad",
                Id = 1,
                Name = "Oats Large Flake",
                Price = 218
            };
            var food2 = new FoodDTO
            {
                Description = "Some Test Description, for FoodDTO2",
                FoodCategoryName = "Drikkevare",
                Id = 2,
                Name = "Oil - Food, Lacquer Spray",
                Price = 202
            };
            var food3 = new FoodDTO
            {
                Description = "Some Test Description, for FoodDTO2",
                FoodCategoryName = "Mad",
                Id = 3,
                Name = "Turkey Leg With Drum And Thigh",
                Price = 179
            };
            var food4 = new FoodDTO
            {
                Description = "Some Test Description, for FoodDTO2",
                FoodCategoryName = "Drikkevare",
                Id = 4,
                Name = "Ecolab - Lime - A - Way 4/4 L",
                Price = 112
            };


            #endregion

            var completeFoodInAList = new List<FoodDTO> { food1, food2, food3, food4 };

            // Act => Assert
            Assert.ThrowsException<FormatException>(() => ConvertStringToFoodLists
                .ListOfFoodsIdStringsToFoodList(listOfFoodIdStrings, completeFoodInAList));

        }
    }
}