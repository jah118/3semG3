using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Autofac.Extras.Moq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using RestaurantWebApp.DataTransferObject;
using RestaurantWebApp.Service;
using RestaurantWebApp.Service.Interfaces;

namespace RestaurantWebApp.Test.Service
{
	[TestClass]
	public class OrderServiceTest
	{
		private readonly OrderService _sut;
		private readonly Mock<IOrderService> _orderServiceMock = new Mock<IOrderService>();

		public OrderServiceTest()
		{
			_sut = new OrderService(_orderServiceMock.Object);
		}

		[TestMethod]
		public void GetById_ShouldReturnOrder_WhenOrder()
		{


			using (var mock = AutoMock.GetLoose())
            {
				// Arrange - configure the mock
				var orderId = 4;
                var employeeID = 1;
                var orderDate = DateTime.Now;
                IEnumerable<OrderLineDTO> orderLines = new List<OrderLineDTO>();
                orderLines.Append(new OrderLineDTO(1, new FoodDTO(1)));
                var reservationID = 1;

                var orderDto = new OrderDTO()
                {
                    OrderNo = orderId,
                    EmployeeID = employeeID,
                    OrderDate = orderDate,
                    OrderLines = orderLines,
                    PaymentCondition = "Begyndt",
                    ReservationID = reservationID,

                };


				mock.Mock<IOrderService>().Setup(x => x.GetById(orderId)).Returns(orderDto);
                var sut = mock.Create<OrderDTO>();

                // Act
                var actual = sut.OrderNo;

                // Assert - assert on the mock
                mock.Mock<IOrderService>().Verify(x => x.GetById(orderId));
                Assert.AreEqual(4, actual);
            }
		}

		[TestMethod]
		public void GetById_ShouldReturnOrder_WhenOrderExists()
		{
			var orderId = 4;
			var employeeID = 1;
			var orderDate = DateTime.Now;
			IEnumerable<OrderLineDTO> orderLines = new List<OrderLineDTO>();
			orderLines.Append(new OrderLineDTO(1, new FoodDTO(1)));
			var reservationID = 1;

			var orderDto = new OrderDTO()
			{
				OrderNo = orderId,
				EmployeeID = employeeID,
				OrderDate = orderDate,
				OrderLines = orderLines,
				PaymentCondition = "Begyndt",
				ReservationID = reservationID,

			};
			_orderServiceMock.Setup(x => x.GetById(orderId)).Returns(orderDto);
			//act
			var order = _sut.GetById(orderId);

			Assert.Equals(orderId, order.OrderNo);


		}



	}
}
