namespace RestaurantWebApp.DataTransferObject
{
    public class OrderLineDTO
    {
        public int Quantity { get; set; }
        public FoodDTO Food { get; set; }
    }
}