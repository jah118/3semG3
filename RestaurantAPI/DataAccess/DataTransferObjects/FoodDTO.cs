namespace DataAccess.DataTransferObjects
{
    public class FoodDTO
    {
        public int Id { get; }
        public string Name { get; set; }
        public string Description { get; set; }
        public FoodCategoryDTO FoodCategoryName { get; set; }
        public decimal Price { get; set; }
    }
}