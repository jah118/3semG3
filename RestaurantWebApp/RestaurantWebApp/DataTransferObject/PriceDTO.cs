namespace RestaurantWebApp.DataTransferObject
{
    public class Price
    {
        public int Id { get; }
        public decimal PriceValue { get; }
        public FoodDTO Food { get; }
    }
}