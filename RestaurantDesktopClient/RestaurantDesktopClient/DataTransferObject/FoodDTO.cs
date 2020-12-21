using Newtonsoft.Json;

namespace RestaurantDesktopClient.DataTransferObject
{
    public class FoodDTO
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }

        public string FoodCategoryName { get; set; }

        [JsonProperty("price")]
        public double Price { get; internal set; }

    }
}
