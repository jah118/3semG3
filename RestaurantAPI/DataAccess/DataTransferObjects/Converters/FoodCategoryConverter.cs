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
        public static FoodCategory Convert(FoodCategoryDTO obj)
        {
            return new FoodCategory()
            {
                Id = obj.Id,
                Name = obj.Name,
                
            };
        }

        public static FoodCategoryDTO Convert(FoodCategory obj)
        {
            return new FoodCategoryDTO()
            {
                Id = obj.Id,
                Name = obj.Name,
                
            };
        }

        public static IEnumerable<FoodCategory> Convert(IEnumerable<FoodCategoryDTO> obj)
        {
            var list = new List<FoodCategory>();
            foreach (var f in obj)
            {
                list.Add(Convert(f));
            }
            return list;
        }

        public static IEnumerable<FoodCategoryDTO> Convert(IEnumerable<FoodCategory> obj)
        {
            var list = new List<FoodCategoryDTO>();
            foreach (var f in obj)
            {
                list.Add(Convert(f));
            }
            return list;
        }
    }
}
