using DataAccess.DataTransferObjects;
using DataAccess.DataTransferObjects.Converters;
using DataAccess.Models;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using SemanticComparison;
using System;
using System.Collections.Generic;
using System.Linq;

namespace DataAccess.Tests
{
    [TestClass, TestCategory("Converters")]
    public class ConversionTest
    {
        #region TestDataMethods
        private List<PriceDTO> GetPriceDTO()
        {
            return new List<PriceDTO>() {
                new PriceDTO
                {
                    Id = 1,
                    PriceValue = 100
                },
                new PriceDTO
                {
                    Id = 2,
                    PriceValue = 200
                }
            };
        }
        private List<Price> GetPrice()
        {
            return new List<Price>() {
                new Price
                {
                    Id = 1,
                    PriceValue = 100,
                    Food = new Food
                    {
                        Description = "Some Test Description, for FoodDTO1",
                        FoodCategory = GetFoodCategory()[0],
                        Id = 1,
                        Name = "FoodName1",
                    }

                },
                new Price
                {
                    Id = 2,
                    PriceValue = 200,
                    Food = new Food
                    {
                        Description = "Some Test Description, for FoodDTO2",
                        FoodCategory = GetFoodCategory()[1],
                        Id = 2,
                        Name = "FoodName2",
                    }
                }
            };
        }
        private List<FoodDTO> GetFoodDTO()
        {
            return new List<FoodDTO>()
            {
                new FoodDTO
                {
                    Description = "Some Test Description, for FoodDTO1",
                    FoodCategoryName = "Mad",
                    Id = 1,
                    Name = "FoodName1",
                    Price = 100
                },
                new FoodDTO
                {
                    Description = "Some Test Description, for FoodDTO2",
                    FoodCategoryName = "Drikkevare",
                    Id = 2,
                    Name = "FoodName2",
                    Price = 200
                },
            };
        }
        private List<Food> GetFood()
        {
            return new List<Food>()
            {
                new Food
                {
                    Description = "Some Test Description, for FoodDTO1",
                    FoodCategory = GetFoodCategory()[0],
                    Id = 1,
                    Name = "FoodName1",
                    Price = GetPrice(),
                },
                new Food
                {
                    Description = "Some Test Description, for FoodDTO2",
                    FoodCategory = GetFoodCategory()[1],
                    Id = 2,
                    Name = "FoodName2",
                    Price = new List<Price>(){GetPrice()[1]}
                },
            };
        }
        private List<FoodCategory> GetFoodCategory()
        {
            return new List<FoodCategory>()
            {
                new FoodCategory
                {
                    Id = 1,
                    Name = "Mad"
                },
                new FoodCategory
                {
                    Id = 2,
                    Name = "Drikkevare"
                }
            };
        }

        private List<RestaurantOrder> GetRestaurantOrder()
        {
            return new List<RestaurantOrder>()
            {
                new RestaurantOrder
                {
                    EmployeeId = 1,
                    OrderDate = DateTime.Now,
                    OrderNo = 1,
                    PaymentCondition = new Models.PaymentCondition{ Condition = "Betalt", Id = 4},
                    ReservationId = 1,
                    Reservation = new Reservation{ Id = 1},
                    OrderLine = new List<OrderLine>()
                    {
                        new OrderLine
                        {
                            Food = GetFood()[0],
                            Quantity = 1
                        },
                        new OrderLine
                        {
                            Food = GetFood()[1],
                            Quantity = 2
                        }
                    }
                },
                new RestaurantOrder
                {
                    EmployeeId = 2,
                    OrderDate = DateTime.Now.AddDays(1),
                    OrderNo = 2,
                    PaymentCondition = new Models.PaymentCondition{ Condition = "Leveret", Id = 3},
                    ReservationId = 2,
                    Reservation = new Reservation{Id = 2 },
                    OrderLine = new List<OrderLine>()
                    {
                        new OrderLine
                        {
                            Food = GetFood()[1],
                            Quantity = 2
                        },
                        new OrderLine
                        {
                            Food = GetFood()[0],
                            Quantity = 3
                        }
                    }
                }
            };
        }
        private List<Reservation> GetReservations()
        {
            return new List<Reservation>()
            {
                new Reservation
                {
                    Deposit = true,
                    Id = 1,
                    NoOfPeople = 4,
                    ReservationDate = DateTime.Now,
                    ReservationTime = DateTime.Now.AddDays(2),
                    Note = "Some note for reservation1",


                },
                new Reservation
                {
                    Deposit = false,
                    Id =2,
                    NoOfPeople = 3,
                    ReservationDate = DateTime.Now.AddDays(1),
                    ReservationTime = DateTime.Now.AddDays(2),
                    Note = "Some note for reservation2",


                }
            };
        }
        #endregion

        #region ReservationConversion

        [TestMethod, TestCategory("Integration"), TestCategory("Converters")]
        public void ReservationConversionReservationListToReservationDTOList()
        {
            //Arrange
            List<Reservation> reservations = GetReservations();
            //Act
            List<ReservationDTO> reservationDTOs = Converter.Convert(reservations).ToList();
            //Assert
            for (int i = 0; i < reservations.Count; i++)
            {
                Assert.AreEqual(reservations.Count, reservationDTOs.Count);
                Assert.AreEqual(reservations[i].Id, reservationDTOs[i].Id);
                Assert.AreEqual(reservations[i].Deposit, reservationDTOs[i].Deposit);
                Assert.AreEqual(reservations[i].Note, reservationDTOs[i].Note);
                Assert.AreEqual(reservations[i].NoOfPeople, reservationDTOs[i].NoOfPeople);
                Assert.AreEqual(reservations[i].ReservationDate, reservationDTOs[i].ReservationDate);
                Assert.AreEqual(reservations[i].ReservationTime, reservationDTOs[i].ReservationTime);
            }
        }

        #endregion

        #region OrderConversion
        [TestMethod, TestCategory("Integration"), TestCategory("Converters")]
        public void OrderConversionOrderListToOrderDTOList()
        {
            //Arrange
            List<RestaurantOrder> orders = GetRestaurantOrder();
            //Act
            List<OrderDTO> orderDTOs = Converter.Convert(orders).ToList();
            //Assert
            for (int i = 0; i < orders.Count; i++)
            {
                Assert.AreEqual(orderDTOs.Count, orders.Count);
                Assert.AreEqual(orders[i].OrderNo, orderDTOs[i].OrderNo);
                Assert.AreEqual(orders[i].PaymentCondition.Condition, orderDTOs[i].PaymentCondition);
                Assert.AreEqual(orders[i].EmployeeId, orderDTOs[i].EmployeeID);
                Assert.AreEqual(orders[i].OrderDate, orderDTOs[i].OrderDate);
                Assert.AreEqual(orders[i].ReservationId, orderDTOs[i].ReservationID);
                orders[i].OrderLine.ToList().ForEach((x) =>
                {
                    Assert.IsNotNull(orderDTOs[i].OrderLines.Where(o => o.Food.Id == x.Food.Id && o.Quantity == x.Quantity).FirstOrDefault());
                });
            }
        }

        #endregion

        #region FoodConversion
        [TestMethod, TestCategory("Integration"), TestCategory("Converters")]
        public void FoodConversionFoodListToFoodDTOList()
        {
            //Arrange
            List<Food> foods = GetFood();
            //Act
            List<FoodDTO> foodDTOs = Converter.Convert(foods).ToList();
            //Assert
            for (int i = 0; i < foods.Count; i++)
            {
                var tempFoodDTO = foodDTOs.Where(x => x.Id == foods[i].Id).FirstOrDefault();
                double expectedPriceValue = Decimal.ToDouble(foods[i].Price.Where(p => p.Food.Id == foods[i].Id).FirstOrDefault().PriceValue);
                Assert.AreEqual(tempFoodDTO.Id, foods[i].Id);
                Assert.AreEqual(tempFoodDTO.Description, foods[i].Description);
                Assert.AreEqual(tempFoodDTO.Name, foods[i].Name);
                Assert.AreEqual(tempFoodDTO.Price, expectedPriceValue);
                Assert.AreEqual(tempFoodDTO.FoodCategoryName, foods[i].FoodCategory.Name);
            }
        }
        [TestMethod, TestCategory("Unit"), TestCategory("Converters")]
        public void FoodConversionFoodToFoodDTO()
        {
            //Arrange
            Food food = GetFood()[0];
            //Act
            FoodDTO foodDTO = Converter.Convert(food);
            var exceptedFoodPrice = Decimal.ToDouble(food.Price.Where(x => x.Food.Id == food.Id).FirstOrDefault().PriceValue);
            //Assert
            Assert.AreEqual(food.Id, foodDTO.Id);
            Assert.AreEqual(food.Description, foodDTO.Description);
            Assert.AreEqual(food.Name, foodDTO.Name);
            Assert.AreEqual(exceptedFoodPrice, foodDTO.Price);
            Assert.AreEqual(food.FoodCategory.Name, foodDTO.FoodCategoryName);
        }
        [TestMethod, TestCategory("Unit"), TestCategory("Converters")]
        public void FoodConversionFoodDTOToFood()
        {
            //Arrange
            FoodDTO foodDTO = GetFoodDTO()[0];
            //Act
            Food food = Converter.Convert(foodDTO);
            //Assert
            Assert.AreEqual(foodDTO.Id, food.Id);
            Assert.AreEqual(foodDTO.Description, food.Description);
        }
        #endregion

        #region priceConversion
        [TestMethod, TestCategory("Unit"), TestCategory("Converters")]
        public void PriceConversionPriceDTOToPrice()
        {
            //Arrange
            PriceDTO priceDTO = GetPriceDTO()[0];
            //Act
            Price price = Converter.Convert(priceDTO);
            //Assert
            Assert.AreEqual(priceDTO.Id, price.Id);
            Assert.AreEqual(priceDTO.PriceValue, price.PriceValue);
        }
        [TestMethod, TestCategory("Unit"), TestCategory("Converters")]
        public void PriceConversionPriceToPriceDTO()
        {
            //Arrange
            Price price = GetPrice()[0];
            //Act
            PriceDTO priceDTO = Converter.Convert(price);
            //Assert
            Assert.AreEqual(price.Id, priceDTO.Id);
            Assert.AreEqual(price.PriceValue, priceDTO.PriceValue);
        }
        [TestMethod, TestCategory("Integration"), TestCategory("Converters")]
        public void PriceConversionPriceDTOListToPriceList()
        {
            //Arrange
            List<PriceDTO> priceDTOs = GetPriceDTO();
            //Act
            var prices = Converter.Convert(priceDTOs);
            //Assert
            for (int i = 0; i < priceDTOs.Count; i++)
            {
                Assert.AreEqual(priceDTOs[i].Id, prices[i].Id);
                Assert.AreEqual(priceDTOs[i].PriceValue, prices[i].PriceValue);
            }
        }
        [TestMethod, TestCategory("Integration"), TestCategory("Converters")]
        public void PriceConversionPriceListToPriceDTOList()
        {
            //Arrange
            List<Price> prices = GetPrice();
            //Act
            var priceDTOs = Converter.Convert(prices);
            //Assert
            for (int i = 0; i < priceDTOs.Count; i++)
            {
                Assert.AreEqual(prices[i].Id, priceDTOs[i].Id);
                Assert.AreEqual(prices[i].PriceValue, priceDTOs[i].PriceValue);
            }
        }
        #endregion

        //[TestMethod, TestCategory("Unit"), TestCategory("Converters")]
        //public void CustomerConversionOutput()
        //{
        //    //Arrange
        //    var customerID = 2;
        //    var personID = 4;
        //    var email = "test@example.com";

        //    var input = new Customer()
        //    {
        //        Id = customerID,
        //        PersonId = personID,
        //        Person = new Person()
        //        {
        //            Id = personID,
        //            Email = email,
        //            FirstName = "",
        //            LastName = "",
        //            Location = new Location()
        //            {
        //                Address = "",
        //                ZipCode = "",

        //            },
        //            LocationId = -1,
        //            Phone = ""
        //        }

        //    };
        //    var expected = new CustomerDTO()
        //    {
        //        Id = customerID,

        //    };
        //    //Act
        //    var output = Converter.Convert(input);
        //    //Assert
        //    //sem
        //    //Assert.IsTrue(output);
        //}

        //[TestMethod, TestCategory("Unit"), TestCategory("Converters")]
        //public void ReservationConversionOutput()
        //{

        //    //Arrange
        //    var input = new Reservation()
        //    {

        //    };
        //    var expected = new ReservationDTO()
        //    {
        //        Id = 3
        //    };
        //    //Act
        //    var output = Converter.Convert(input);
        //    //Assert
        //    Assert.IsTrue(true);
        //}

        //[TestMethod, TestCategory("Unit"), TestCategory("Converters")]
        //public void EmployeeConversionOutput()
        //{

        //    //Arrange
        //    var input = new Reservation()
        //    {

        //    };
        //    var expected = new ReservationDTO()
        //    {
        //        Id = 3
        //    };
        //    //Act
        //    var output = Converter.Convert(input);
        //    //Assert
        //    Assert.IsTrue(true);
        //}

        //[TestMethod, TestCategory("Unit"), TestCategory("Converters")]
        //public void TableConversionOutput()
        //{

        //    //Arrange
        //    var input = new Reservation()
        //    {

        //    };
        //    var expected = new ReservationDTO()
        //    {
        //        Id = 3
        //    };
        //    //Act
        //    var output = Converter.Convert(input);
        //    //Assert
        //    Assert.IsTrue(true);
        //}

    }
}