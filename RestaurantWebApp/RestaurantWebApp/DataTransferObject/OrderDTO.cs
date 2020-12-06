using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace RestaurantWebApp.DataTransferObject
{
    public class OrderDTO
    {
        public OrderDTO()
        { 
        }

        public OrderDTO(int reservationId, DateTime orderDate, string paymentCondition, IEnumerable<OrderLineDTO> orderLines)
        {
            ReservationID = reservationId;
            OrderDate = orderDate;
            PaymentCondition = paymentCondition;
            OrderLines = orderLines;
        }
        public int OrderNo { get; set; }
        public int ReservationID { get; set; }
        public int EmployeeID { get; set; }
        public DateTime OrderDate { get; set; }
        public string PaymentCondition { get; set; }
        public IEnumerable<OrderLineDTO> OrderLines { get; set; }
    }
}