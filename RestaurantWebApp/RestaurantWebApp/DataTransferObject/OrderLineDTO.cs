﻿namespace RestaurantWebApp.DataTransferObject
{
    public class OrderLineDTO
    {
        public OrderLineDTO(int quantity, FoodDTO food)
        {
            Quantity = quantity;
            Food = food;
        }

        public int Quantity { get; set; }
        public FoodDTO Food { get; set; }
    }
}