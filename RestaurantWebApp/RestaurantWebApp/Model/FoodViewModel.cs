using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace RestaurantWebApp.Model
{
    public class FoodViewModel
    {
        //TODO needs to be cleaned up.
        public IEnumerable<DataTransferObject.FoodDTO> Foods { get; set;}
        public DataTransferObject.FoodDTO SelectedFood { get; set; }

        public IEnumerable<DataTransferObject.FoodDTO> Drinks { get; set; }
        public DataTransferObject.FoodDTO SelectedDrink { get; set; }

        public IEnumerable<DataTransferObject.FoodDTO> OrderFoodAndDrinks { get; set; }
        public DataTransferObject.FoodDTO SelectedOrderFoodAndDrinks { get; set; }

        public DataTransferObject.OrderDTO Order { get; set; }

    }
}