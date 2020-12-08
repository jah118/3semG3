using System;
using System.Collections.Generic;

namespace DataAccess.DataTransferObjects
{
    public class OrderDTO
    {
        public Nullable<int> OrderNo { get; init; }
        public int ReservationID { get; set; }
        public int EmployeeID { get; set; }
        public DateTime OrderDate { get; set; }
        public string PaymentCondition { get; set; }

        public ICollection<OrderLineDTO> OrderLines { get; set; }
    }
}