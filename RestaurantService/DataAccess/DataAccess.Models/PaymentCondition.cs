using System;
using System.Collections.Generic;

namespace DataAccess.DataAccess.Models
{
    public partial class PaymentCondition
    {
        public PaymentCondition()
        {
            RestaurantOrder = new HashSet<RestaurantOrder>();
        }

        public int Id { get; set; }
        public string Condition { get; set; }

        public virtual ICollection<RestaurantOrder> RestaurantOrder { get; set; }
    }
}
