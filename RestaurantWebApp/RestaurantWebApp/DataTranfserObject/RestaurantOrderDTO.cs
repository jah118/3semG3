using System;

namespace DataAccess.DataTransferObjects
{
    public class RestaurantOrderDTO
    {

        public int OrderNo { get; }
        public  ReservationDTO reservation { get; set; }
        public EmployeeDTO employee { get;  }
        public DateTime OrderDate { get; set; }
        public PaymentCondition paymentCondition { get; set; }

        //public virtual Employee Employee { get; set; }
        //public virtual PaymentCondition PaymentCondition { get; set; }
        //public virtual Reservation Reservation { get; set; }
        //public virtual ICollection<OrderLine> OrderLine { get; set; /*}*/
    }
}