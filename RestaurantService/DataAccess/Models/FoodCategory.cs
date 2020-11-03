using System;
using System.Collections.Generic;

namespace DataAccess.Models
{
    public partial class FoodCategory
    {
        public FoodCategory()
        {
            Food = new HashSet<Food>();
        }

        public int Id { get; set; }
        public string Name { get; set; }

        public virtual ICollection<Food> Food { get; set; }
    }
}
