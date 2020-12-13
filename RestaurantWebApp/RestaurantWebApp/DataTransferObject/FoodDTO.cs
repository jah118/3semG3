namespace RestaurantWebApp.DataTransferObject
{
    public class FoodDTO
    {
        public FoodDTO()
        {
        }

        public FoodDTO(int id)
        {
            Id = id;
        }

        public FoodDTO(int id, string name, string description, string foodCategoryName, double price)
        {
            Id = id;
            Name = name;
            Description = description;
            FoodCategoryName = foodCategoryName;
            Price = price;
        }

        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string FoodCategoryName { get; set; }
        public double Price { get; set; }

        public bool Equals(FoodDTO other)
        {
            if (other == null) return false;
            return Id.Equals(other.Id);
        }

        public override bool Equals(object obj)
        {
            if (obj == null) return false;
            var objAsFoodDTO = obj as FoodDTO;
            if (objAsFoodDTO == null) return false;
            return Equals(objAsFoodDTO);
        }

        public override int GetHashCode()
        {
            return Id;
        }
    }
}