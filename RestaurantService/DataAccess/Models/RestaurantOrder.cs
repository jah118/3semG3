using System;
using System.Collections.Generic;

namespace DataAccess.Models
{
    public partial class RestaurantOrder
    {
        public RestaurantOrder()
        {
            OrderLine = new HashSet<OrderLine>();
        }

        public int OrderNo { get; set; }
        public int ReservationId { get; set; }
        public int EmployeeId { get; set; }
        public DateTime OrderDate { get; set; }
        public int PaymentConditionId { get; set; }

        public virtual Employee Employee { get; set; }
        public virtual PaymentCondition PaymentCondition { get; set; }
        public virtual Reservation Reservation { get; set; }
        public virtual ICollection<OrderLine> OrderLine { get; set; }
    }
}
