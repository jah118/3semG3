﻿using DataAccess.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.DataTransferObjects.Converters
{
    public partial class Converter
    {

        public static OrderDTO Convert(RestaurantOrder obj)
        {
            return new OrderDTO()
            {
                EmployeeID = obj.EmployeeId,
                OrderDate = obj.OrderDate,
                OrderNo = obj.OrderNo,
                PaymentCondition = obj.PaymentCondition.Condition,
                ReservationID = obj.Reservation.Id,
                OrderLines = Convert(obj.OrderLine)
                //OrderLines = (ICollection<OrderLineDTO>)obj.OrderLine
                //OrderLines = new List<OrderLineDTO>()
            };
            //OrderDTO orderDTO = new OrderDTO()
            //{
            //    EmployeeID = obj.EmployeeId,
            //    OrderDate = obj.OrderDate,
            //    OrderNo = obj.OrderNo,
            //    PaymentCondition = obj.PaymentCondition.Condition,
            //    ReservationID = obj.Reservation.Id,
            //    OrderLines = new List<OrderLineDTO>()
            //};
            //obj.OrderLine.ToList().ForEach(x => orderDTO.OrderLines.Add(new OrderLineDTO { Food = Converter.Convert(x.Food), Quantity = x.Quantity }));
            //return orderDTO;
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
            order.OrderLine = obj.OrderLines.Select(x => new OrderLine()
            {
                FoodId = x.Food.Id,
                Quantity = x.Quantity,
                OrderNumber = obj.OrderNo,
                OrderNumberNavigation = order
            }).ToHashSet();
            return order;
        }
        public static IEnumerable<OrderDTO> Convert(List<RestaurantOrder> obj)
        {
            var res = new List<OrderDTO>();
            foreach (var item in obj)
            {
                res.Add(Convert(item));
            }
            //obj.ForEach((x) =>
            //{
            //    res.Add(Convert(x));
            //});
            return res;
        }
    }
}
