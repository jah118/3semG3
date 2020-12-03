using DataAccess.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.DataTransferObjects.Converters
{
    public partial class Converter 
    {
        //POST
        public static Food Convert(FoodDTO obj)
        {
            

            return new Food()

            {
                Id = obj.Id,
                Name = obj.Name,
                Description = obj.Description,
               


            };
            
        }
        //GET

        public static FoodDTO  Convert(Food obj)
        {
            var priceobj = obj.Price.Where(p => p.FoodId == obj.Id).FirstOrDefault();
            var orderLineobj = obj.OrderLine.FirstOrDefault();
            return new FoodDTO()
            {
                Id = obj.Id,
                Name = obj.Name,
                Description = obj.Description,
                Price = priceobj != null ? Decimal.ToDouble(Convert(priceobj).PriceValue) : 0,
                FoodCategoryName = obj.FoodCategory.Name,
            };
        }

        public static IEnumerable<FoodDTO> Convert(IEnumerable<Food> obj)
        {
            var list = new List<FoodDTO>();
            foreach (var f in obj)
            {
                list.Add(Convert(f));
            }
            return list;
        }
    }
}
