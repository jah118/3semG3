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
        public IEnumerable<RestaurantWebApp.DataTransferObject.FoodDTO> VmF { get; set;}
        //public IEnumerable<SelectListItem> Slf { get; set; }

        //public FoodDTO SelectedFood { get; set; }

        public IEnumerable<RestaurantWebApp.DataTransferObject.FoodDTO> VmD { get; set; }
        //public IEnumerable<SelectListItem> Sld { get; set; }
        public FoodDTO SelectedDrink { get; set; }

        public IEnumerable<RestaurantWebApp.DataTransferObject.FoodDTO> VmO { get; set; }
        //public DataTransferObject.FoodDTO SelectedOrderFoodAndDrinks { get; set; }

        //public OrderDTO VmOrder { get; set; }
        //public IEnumerable<OrderDTO> VmOrder { get; set; }

    }
}