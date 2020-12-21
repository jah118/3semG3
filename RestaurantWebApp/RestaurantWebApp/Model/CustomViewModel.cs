using RestaurantWebApp.DataTransferObject;
using System.Collections.Generic;

namespace RestaurantWebApp.Model
{
    public class CustomViewModel
    {
        public IList<FoodDTO> ListFood { get; set; }
        public IList<FoodDTO> ListDrink { get; set; }
        public IList<FoodDTO> OrderSummary { get; set; }
        public ReservationDTO Reservation { get; set; }
    }
}