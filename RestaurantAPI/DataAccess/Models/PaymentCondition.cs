using System.Collections.Generic;

namespace DataAccess.Models
{
    public partial class PaymentCondition
    {
        public PaymentCondition()
        {
            RestaurantOrder = new HashSet<Order>();
        }

        public int Id { get; set; }
        public string Condition { get; set; }

        public virtual ICollection<Order> RestaurantOrder { get; set; }
    }
}