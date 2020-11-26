using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.DataTransferObjects
{
    public class FoodDTO
    {
        public int Id { get; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string foodCategoryName { get; set; }
        public double Price { get; internal set; }
        public int Quantity { get; set; }
    }
}
