using System;
using System.Collections.Generic;

namespace DataAccess.DataTransferObjects
{
    public class OrderLineDTO
    {
        public int Quantity { get; set; }
        public int FoodId { get; }
        public int OrderNumber { get; }

        public FoodDTO Food { get; set; }
        public RestaurantOrderDTO OrderNumberNavigation { get;  }
    }
}
