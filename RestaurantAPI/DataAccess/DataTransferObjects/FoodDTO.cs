using DataAccess.Models;

namespace DataAccess.DataTransferObjects
{
    public class FoodDTO
    {
        public int Id { get; init; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string FoodCategoryName { get; set; }
        public double Price { get; set; }
    }
}