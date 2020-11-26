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
        public static IEnumerable<OrderDTO> Convert(List<Order> obj)
        {
            List<OrderDTO> res = new List<OrderDTO>();
            obj.ForEach((x) =>
            {
                res.Add(Convert(x)) ;
            });
            return res;
        }
        public static OrderDTO Convert(Order obj)
        {
            return new OrderDTO()
            {
                Employee = Converter.Convert(obj.Employee),
                OrderDate = obj.OrderDate,
                OrderNo = obj.OrderNo,
                PaymentCondition = obj.PaymentCondition.Condition,
                ReservationID = obj.Reservation.Id
            };
        }
    }
}
