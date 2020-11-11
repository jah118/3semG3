﻿using System;
using System.Collections.Generic;

namespace DataAccess.DataTransferObjects
{
    public class OrderLineDTO
    {
        public int Quantity { get; set; }
        public int FoodId { get; set; }
        public int OrderNumber { get; set; }

        public FoodDTO Food { get; set; }
        public RestaurantOrderDTO OrderNumberNavigation { get; set; }
    }
}