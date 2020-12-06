using System.Collections.Generic;
using System.Linq;
using DataAccess.Models;

namespace DataAccess.DataTransferObjects.Converters
{
    public partial class Converter
    {
        //POST
        public static Food Convert(FoodDTO obj)
        {
            var fc = new FoodCategoryDTO(obj.FoodCategoryName);

            return new Food
            {
                Id = obj.Id,
                Name = obj.Name,
                Description = obj.Description
                //FoodCategory = Convert(fc), //TODO need to be check if it is needed ?
                //Price = Convert(new PriceDTO( obj.Price)) //TODO is this needed ?
            };
        }

        //GET

        public static FoodDTO Convert(Food obj)
        {
            var priceobj = obj.Price.Where(p => p.FoodId == obj.Id).FirstOrDefault();
            var orderLineobj = obj.OrderLine.FirstOrDefault();
            return new FoodDTO
            {
                Id = obj.Id,
                Name = obj.Name,
                Description = obj.Description,
                Price = priceobj != null ? decimal.ToDouble(Convert(priceobj).PriceValue) : 0,
                FoodCategoryName = obj.FoodCategory.Name
            };
        }

        public static IEnumerable<FoodDTO> Convert(IEnumerable<Food> obj)
        {
            var list = new List<FoodDTO>();
            foreach (var f in obj) list.Add(Convert(f));
            return list;
        }
    }
}