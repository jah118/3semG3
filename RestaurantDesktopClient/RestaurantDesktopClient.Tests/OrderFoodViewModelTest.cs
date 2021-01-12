using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using RestaurantDesktopClient.DataTransferObject;
using RestaurantDesktopClient.Messages;
using RestaurantDesktopClient.Services;
using RestaurantDesktopClient.Views.ViewModels;
using Moq;

namespace RestaurantDesktopClient.Tests
{
    [TestClass]
    public class OrderFoodViewModelTest
    {
        [TestInitialize]
        public void InitializeData()
        {
            ResetOrderRepository();
            ResetOrderFoodModelView();
        }
        [TestMethod]

        public void ChangeReservationSuccess()
        {
            //Arrange

            var orderFoodViewModel = OrderFoodViewModel;
            OrderDTO order = GetOrders().First();
            var reservationSelection = new ReservationSelection { Selected = order.ReservationID };


            //Assert
            orderFoodViewModel.ChangeReservation(reservationSelection);
            var res = orderFoodViewModel.SummaryFoods;


            //Act
            Assert.AreEqual(res.Count, order.OrderLines.Count);
            for (int i = 0; i < res.Count; i++)
            {
                var orderLineDto = res[i];
                Assert.AreEqual(orderLineDto.Food.Id, order.OrderLines[i].Food.Id);
                Assert.AreEqual(orderLineDto.Quantity, order.OrderLines[i].Quantity);
            }
        }

        #region TestData

        private OrderFoodViewModel _orderFoodViewModel;
        private Mock<IRepository<OrderDTO>> _orderRepositoryMock;

        private Mock<IRepository<OrderDTO>> OrderRepositoryMock
        {
            get
            {
                if (_orderRepositoryMock == null) ResetOrderRepository();

                return _orderRepositoryMock;
            }
        }

        private OrderFoodViewModel OrderFoodViewModel
        {
            get
            {
                if (_orderFoodViewModel == null) ResetOrderFoodModelView();

                return _orderFoodViewModel;
            }
        }

        private void ResetOrderRepository()
        {
            //Mock repository, to control content of data
            if (_orderRepositoryMock == null) _orderRepositoryMock = new Mock<IRepository<OrderDTO>>();
            _orderRepositoryMock.Setup(x => x.GetAll()).Returns(GetOrders);
        }
        private void ResetOrderFoodModelView()
        {
            var foodRepostitoryMock = new Mock<IRepository<FoodDTO>>();
            _orderFoodViewModel = new OrderFoodViewModel(foodRepostitoryMock.Object, _orderRepositoryMock.Object);
        }

        private IEnumerable<FoodDTO> GetFoods()
        {
            return new List<FoodDTO>
            {
                new FoodDTO
                {
                    Id = 1,
                    FoodCategoryName = "Mad",
                    Description = "Some food",
                    Name = "Some Owesome food"
                },
                new FoodDTO
                {
                Id = 2,
                FoodCategoryName = "Drikkevare",
                Description = "Some drink",
                Name = "Some Owesome drink"
            }
            };
        }

        private IEnumerable<OrderDTO> GetOrders()
        {
            return new List<OrderDTO>
            {
                new OrderDTO()
                {
                    ReservationID = 1,
                    OrderLines = new List<OrderLineDTO>
                    {
                        new OrderLineDTO()
                        {
                            Food = GetFoods().First(),
                            Quantity = 2
                        },
                        new OrderLineDTO()
                        {
                            Food = GetFoods().ElementAt(1),
                            Quantity = 4
                        }
                    },
                    EmployeeID = 2,
                    OrderDate = DateTime.Now.AddDays(2),
                    OrderNo = 3,
                    PaymentCondition = "Begyndt",

                },
                new OrderDTO()
                {
                    ReservationID = 5,
                    OrderLines = new List<OrderLineDTO>
                    {
                        new OrderLineDTO()
                        {
                            Food = GetFoods().ElementAt(0),
                            Quantity = 1
                        },
                        new OrderLineDTO()
                        {
                            Food = GetFoods().ElementAt(1),
                            Quantity = 3
                        }
                    },
                    EmployeeID = 6,
                    OrderDate = DateTime.Now.AddDays(2),
                    OrderNo = 7,
                    PaymentCondition = "Bestilt",

                }
            };
        }

        #endregion
    }
}
