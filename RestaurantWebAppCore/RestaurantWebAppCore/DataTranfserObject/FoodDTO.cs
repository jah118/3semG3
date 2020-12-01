using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataTransferObjects
{
    public class FoodDTO
    {
        public FoodDTO(int id, string name)
        {
            Id = id;
            Name = name;
        }
        public FoodDTO()
        {

        }
        public int Id { get; }
        
        [Display (Name = "")]
        public string Name {set;  get; }
        public string Description { get; set; }
        public FoodCategoryDTO FoodCategoryName { get; set; }
        public PriceDTO Price { get; set; }
    }
}
