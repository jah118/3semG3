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
            Foods = new List<FoodDTO>();
        }
        public int OrderNo { get; set; }
        public int ReservationID { get; set; }
        public int EmployeeID { get; set; }
        public DateTime OrderDate { get; set; }
        public string PaymentCondition { get; set; }
        public List<FoodDTO> Foods { get; set; }
    }
}