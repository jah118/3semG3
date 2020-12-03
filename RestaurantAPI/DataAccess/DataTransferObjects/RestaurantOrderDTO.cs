using System;

namespace DataAccess.DataTransferObjects
{
    public class RestaurantOrderDTO
    {
        //Needs a second pass when use case calls for it.
        public int OrderNo { get; init; }

        public ReservationDTO reservation { get; set; }
        public EmployeeDTO employee { get; set; }
        public DateTime OrderDate { get; set; }
        public PaymentCondition paymentCondition { get; set; }

        //public virtual Employee Employee { get; set; }
        //public virtual PaymentCondition PaymentCondition { get; set; }
        //public virtual Reservation Reservation { get; set; }
        //public virtual ICollection<OrderLine> OrderLine { get; set; /*}*/
    }
}