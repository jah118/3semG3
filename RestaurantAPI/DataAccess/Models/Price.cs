namespace DataAccess.Models
{
    public partial class Price
    {
        public int Id { get; set; }
        public decimal PriceValue { get; set; }
        public int FoodId { get; set; }
        public virtual Food Food { get; set; }
    }
}