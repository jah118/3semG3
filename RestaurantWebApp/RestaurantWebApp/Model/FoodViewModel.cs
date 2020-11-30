using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace RestaurantWebApp.Model
{
    public class FoodViewModel
    {
        public IEnumerable<DataTransferObject.FoodDTO> Foods { get; set;}
        public DataTransferObject.FoodDTO Food { get; set; }

        public IEnumerable<DataTransferObject.FoodDTO> Drinks { get; set; }
        public DataTransferObject.FoodDTO Drink { get; set; }

    }
}