using DataAccess.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.DataTransferObjects.Converters
{
    public partial class Converter
    {
        public static IEnumerable<OrderDTO> Convert(List<RestaurantOrder> obj)
        {
            List<OrderDTO> res = new List<OrderDTO>();
            obj.ForEach((x) =>
            {
                res.Add(Convert(x));
            });
            return res;
        }
        public static OrderDTO Convert(RestaurantOrder obj)
        {
            OrderDTO orderDTO = new OrderDTO()
            {
                EmployeeID = obj.EmployeeId,
                OrderDate = obj.OrderDate,
                OrderNo = obj.OrderNo,
                PaymentCondition = obj.PaymentCondition.Condition,
                ReservationID = obj.Reservation.Id,
                Foods = new List<FoodDTO>()
            };
            obj.OrderLine.ToList().ForEach(x => orderDTO.Foods.Add(Converter.Convert(x.Food)));
            return orderDTO;
        }
        public static RestaurantOrder Convert(OrderDTO obj)
        {
            RestaurantOrder order = new RestaurantOrder()
            {
                EmployeeId = obj.EmployeeID,
                OrderDate = obj.OrderDate,
                OrderNo = obj.OrderNo,
                ReservationId = obj.ReservationID
            };
            order.OrderLine = obj.Foods.Select(x => new OrderLine()
            {
                FoodId = x.Id,
                Quantity = x.Quantity,
                OrderNumber = obj.OrderNo,
                OrderNumberNavigation = order
            }).ToHashSet();
            return order;
        }
    }
}
