using System.Collections.Generic;

namespace DataAccess.Models
{
    public partial class Food
    {
        public Food()
        {
            OrderLine = new HashSet<OrderLine>();
            Price = new HashSet<Price>();
        }

        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public int FoodCategoryId { get; set; }

        public virtual FoodCategory FoodCategory { get; set; }
        public virtual ICollection<OrderLine> OrderLine { get; set; }
        public virtual ICollection<Price> Price { get; set; }
    }
}