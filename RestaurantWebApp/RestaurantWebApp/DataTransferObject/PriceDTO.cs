namespace RestaurantWebApp.DataTransferObject
{
    public partial class Price
    {
        public int Id { get; }
        public decimal PriceValue { get;  }
        public FoodDTO Food { get;  }
    }
}
