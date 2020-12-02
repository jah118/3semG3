using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace RestaurantWebApp.Model
{
    public class FoodViewModel
    {
        public IEnumerable<DataTransferObject.FoodDTO> Foods { get; set;}
        public DataTransferObject.FoodDTO SelectedFood { get; set; }

        public IEnumerable<DataTransferObject.FoodDTO> Drinks { get; set; }
        public DataTransferObject.FoodDTO SelectedDrink { get; set; }

        public IEnumerable<DataTransferObject.FoodDTO> OrderFoodAndDrinks { get; set; }
        public DataTransferObject.FoodDTO SelectedOrderFoodAndDrinks { get; set; }

    }
}