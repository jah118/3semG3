namespace RestaurantWebApp.DataTransferObject
{
    public class FoodDTO
    {
        public FoodDTO()
        {

        }
        public int Id { get; }
        public string Name { get; }
        public string Description { get; }
        public FoodCategoryDTO FoodCategoryName { get;  }
        public decimal Price { get; set; }
    }
}
