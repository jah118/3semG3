namespace DataAccess.DataTransferObjects
{
    public partial class Price
    {
        // DO NOT USE UNTIL WE FIGURE OUT IT's NEEDED
        public int Id { get; }

        public decimal PriceValue { get; set; }
        public FoodDTO Food { get; set; }
    }
}