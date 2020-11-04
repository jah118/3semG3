using System;
using System.Collections.Generic;

namespace DataAccess.Models
{
    public partial class OrderLine
    {
        public int Quantity { get; set; }
        public int FoodId { get; set; }
        public int OrderNumber { get; set; }

        public virtual Food Food { get; set; }
        public virtual RestaurantOrder OrderNumberNavigation { get; set; }
    }
}
