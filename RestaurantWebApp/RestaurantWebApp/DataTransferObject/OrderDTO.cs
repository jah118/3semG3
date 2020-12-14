using System;
using System.Collections.Generic;

namespace RestaurantWebApp.DataTransferObject
{
    public class OrderDTO
    {
        public OrderDTO()
        {
        }

        public int OrderNo { get; set; }
        public int ReservationID { get; set; }
        public int EmployeeID { get; set; }
        public DateTime OrderDate { get; set; }
        public string PaymentCondition { get; set; }
        public IEnumerable<OrderLineDTO> OrderLines { get; set; }
    }
}