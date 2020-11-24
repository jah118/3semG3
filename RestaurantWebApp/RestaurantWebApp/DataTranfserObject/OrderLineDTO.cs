using System;
using System.Collections.Generic;

namespace DataTransferObjects
{
    public class OrderLineDTO
    {
        public int Quantity { get; set; }
        public FoodDTO Food { get; set; }
    }
}
