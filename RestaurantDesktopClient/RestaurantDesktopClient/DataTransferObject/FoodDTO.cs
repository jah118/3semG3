using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.DataTransferObjects
{
    public class FoodDTO
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string foodCategoryName { get; set; }
        public double Price { get; internal set; }
        public int Quantity { get; set; }

        public FoodDTO(int id, string name, string description, string foodCategoryName, double price, int quantity)
        {
            Id = id;
            Name = name;
            Description = description;
            this.foodCategoryName = foodCategoryName;
            Price = price;
            Quantity = quantity;
        }
    }
}
