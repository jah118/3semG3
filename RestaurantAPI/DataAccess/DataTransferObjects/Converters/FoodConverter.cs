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
            return new FoodDTO()
            {
                Id = obj.Id,
                Name = obj.Name,
                Description = obj.Description,
                Price = decimal.ToDouble(Convert(obj.Price.Where(p => p.FoodId == obj.Id).FirstOrDefault()).PriceValue),
                FoodCategoryName = Convert(obj.FoodCategory).Name,
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
