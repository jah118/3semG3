using System;

namespace RestaurantWebApp.DataTransferObject
{
    public class FoodDTO : IEquatable<FoodDTO>
    {
        public FoodDTO(int id)
        {
            Id = id;
        }

        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string FoodCategoryName { get; set; }
        public double Price { get; set; }

        public bool Equals(FoodDTO other)
        {
            if (other == null) return false;
            return (this.Id.Equals(other.Id));
        }

        public override bool Equals(object obj)
        {
            if (obj == null) return false;
            FoodDTO objAsFoodDTO = obj as FoodDTO;
            if (objAsFoodDTO == null) return false;
            return Equals(objAsFoodDTO);
        }

        public override int GetHashCode()
        {
            return Id;
        }
    }
}