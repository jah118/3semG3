using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace RestaurantWebApp.Model
{
    public class FoodViewModel
    {
        public IEnumerable<DataTransferObjects.FoodDTO> Foods { get; set;}
        public DataTransferObjects.FoodDTO Food { get; set; }

        public IEnumerable<DataTransferObjects.FoodDTO> Drinks { get; set; }
        public DataTransferObjects.FoodDTO Drink { get; set; }

    }
}