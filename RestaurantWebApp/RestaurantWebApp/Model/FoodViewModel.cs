using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using RestaurantWebApp.DataTransferObject;

namespace RestaurantWebApp.Model
{
    public class FoodViewModel
    {
        //TODO needs to be cleaned up.
        public ReservationDTO VmReservation { get; set; }
        public IEnumerable<FoodDTO> VmFoods { get; set;}
        //public DataTransferObject.FoodDTO SelectedFood { get; set; }

        public IEnumerable<FoodDTO> VmDrinks { get; set; }
        //public DataTransferObject.FoodDTO SelectedDrink { get; set; }

        public IEnumerable<FoodDTO> VmOrderFoodAndDrinks { get; set; }
        //public DataTransferObject.FoodDTO SelectedOrderFoodAndDrinks { get; set; }

        public OrderDTO VmOrder { get; set; }

    }
}