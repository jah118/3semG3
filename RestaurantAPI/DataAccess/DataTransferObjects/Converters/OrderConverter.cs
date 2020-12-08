using System.Collections.Generic;
using DataAccess.Models;

namespace DataAccess.DataTransferObjects.Converters
{
    public partial class Converter
    {
        public static OrderDTO Convert(RestaurantOrder obj)
        {
            return new OrderDTO
            {
                EmployeeID = obj.EmployeeId,
                OrderDate = obj.OrderDate,
                OrderNo = obj.OrderNo,
                PaymentCondition = obj.PaymentCondition.Condition,
                ReservationID = obj.Reservation.Id,
                OrderLines = Convert(obj.OrderLine)
            };
        }

        public static RestaurantOrder Convert(OrderDTO obj)
        {
            return new RestaurantOrder
            {
                OrderNo = obj.OrderNo ?? 0,
                ReservationId = obj.ReservationID,
                EmployeeId = obj.EmployeeID,
                OrderDate = obj.OrderDate,
                PaymentConditionId = Convert(new PaymentConditionDTO(obj.PaymentCondition)).Id,
                OrderLine = Convert(obj.OrderLines)
            };
        }


        public static IEnumerable<OrderDTO> Convert(List<RestaurantOrder> obj)
        {
            var res = new List<OrderDTO>();
            foreach (var item in obj) res.Add(Convert(item));
            return res;
        }
    }
}