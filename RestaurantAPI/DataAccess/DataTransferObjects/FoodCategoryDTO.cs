namespace DataAccess.DataTransferObjects
{
    public class FoodCategoryDTO
    {
        public FoodCategoryDTO()
        {
        }

        public FoodCategoryDTO(string name)
        {
            Name = name;
        }

        public int Id { get; set; }

        public string Name { get; set; }
    }
}