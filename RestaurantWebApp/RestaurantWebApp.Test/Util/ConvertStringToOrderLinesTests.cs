using Microsoft.VisualStudio.TestTools.UnitTesting;
using RestaurantWebApp.DataTransferObject;
using RestaurantWebApp.Util;
using System;
using System.Collections.Generic;
using System.Linq;

namespace RestaurantWebApp.Test.Util
{
    [TestClass()]
    public class ConvertStringToOrderLinesTests
    {
        [TestMethod()]
        public void ListOfFoodsIdToOrderLinesTest__withValidInput()
        {
            //Arrange
            const string stringFoodId1 = "1";
            const string stringFoodId2 = "2";
            const string stringFoodId3 = "3";
            const string stringFoodId4 = "4";
            var listOfFoodIdStrings = new List<string> { stringFoodId1, stringFoodId2, stringFoodId3, stringFoodId3, stringFoodId3, stringFoodId4 };

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

            #endregion Setup4FoodDto

            #region Setup4OrderLineDto

            var orderLine1 = new OrderLineDTO(1, food1);
            var orderLine2 = new OrderLineDTO(1, food2);
            var orderLine3 = new OrderLineDTO(3, food3);
            var orderLine4 = new OrderLineDTO(1, food4);

            #endregion Setup4OrderLineDto

            var refOrdlines = new List<OrderLineDTO> { orderLine1, orderLine2, orderLine3, orderLine4 };
            var completeFoodInAList = new List<FoodDTO> { food1, food2, food3, food4 };

            //Act
            var ordlines = ConvertStringToOrderLines.ListOfFoodsIdToOrderLines(listOfFoodIdStrings, completeFoodInAList);

            //Assert
            Assert.IsNotNull(ordlines);
            Assert.AreEqual(4, ordlines.Count());

            Assert.IsNotNull(ordlines.ElementAt(0));
            Assert.IsNotNull(ordlines.ElementAt(1));
            Assert.IsNotNull(ordlines.ElementAt(2));
            Assert.IsNotNull(ordlines.ElementAt(3));

            Assert.AreEqual(refOrdlines.ElementAt(0).Quantity, ordlines.ElementAt(0).Quantity);
            Assert.AreEqual(refOrdlines.ElementAt(1).Quantity, ordlines.ElementAt(1).Quantity);
            Assert.AreEqual(refOrdlines.ElementAt(2).Quantity, ordlines.ElementAt(2).Quantity);
            Assert.AreEqual(refOrdlines.ElementAt(3).Quantity, ordlines.ElementAt(3).Quantity);

            Assert.AreEqual(refOrdlines.ElementAt(0).Food, ordlines.ElementAt(0).Food);
            Assert.AreEqual(refOrdlines.ElementAt(1).Food, ordlines.ElementAt(1).Food);
            Assert.AreEqual(refOrdlines.ElementAt(2).Food, ordlines.ElementAt(2).Food);
            Assert.AreEqual(refOrdlines.ElementAt(3).Food, ordlines.ElementAt(3).Food);
        }

        [TestMethod()]
        public void ListOfFoodsIdToOrderLinesTest__withBadInput()
        {
            //Arrange
            const string stringFoodId1 = "1";
            const string stringFoodId2 = "      ";
            const string stringFoodId3 = "3";
            const string stringFoodId4 = "4";
            var listOfFoodIdStrings = new List<string> { stringFoodId1, stringFoodId2, stringFoodId3, stringFoodId3, stringFoodId3, stringFoodId4 };

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

            #endregion Setup4FoodDto

            #region Setup4OrderLineDto

            var orderLine1 = new OrderLineDTO(1, food1);
            var orderLine2 = new OrderLineDTO(1, food2);
            var orderLine3 = new OrderLineDTO(3, food3);
            var orderLine4 = new OrderLineDTO(1, food4);

            #endregion Setup4OrderLineDto

            var refOrdlines = new List<OrderLineDTO> { orderLine1, orderLine2, orderLine3, orderLine4 };
            var completeFoodInAList = new List<FoodDTO> { food1, food2, food3, food4 };

            // Act => Assert
            Assert.ThrowsException<FormatException>(() => ConvertStringToOrderLines.ListOfFoodsIdToOrderLines(listOfFoodIdStrings, completeFoodInAList));
        }

        [TestMethod()]
        public void ListOfFoodsIdToOrderLinesTest_withNoInput()
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

            #endregion Setup4FoodDto

            #region Setup4OrderLineDto

            var orderLine1 = new OrderLineDTO(1, food1);
            var orderLine2 = new OrderLineDTO(1, food2);
            var orderLine3 = new OrderLineDTO(3, food3);
            var orderLine4 = new OrderLineDTO(1, food4);

            #endregion Setup4OrderLineDto

            var refOrdlines = new List<OrderLineDTO> { orderLine1, orderLine2, orderLine3, orderLine4 };
            var completeFoodInAList = new List<FoodDTO> { food1, food2, food3, food4 };

            // Act => Assert
            Assert.ThrowsException<FormatException>(() => ConvertStringToOrderLines.ListOfFoodsIdToOrderLines(listOfFoodIdStrings, completeFoodInAList));
        }
    }
}