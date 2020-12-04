using System;
using System.Collections.Generic;

namespace RestaurantClientService.DataTransferObjects
{
    public partial class Price
    {
        public int Id { get; }
        public decimal PriceValue { get; set; }
        public FoodDTO Food { get; set; }
    }
}
