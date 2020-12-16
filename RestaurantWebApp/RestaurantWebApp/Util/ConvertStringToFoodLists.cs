using RestaurantWebApp.DataTransferObject;
using System;
using System.Collections.Generic;
using Castle.Core.Internal;

namespace RestaurantWebApp.Util
{
    public static class ConvertStringToFoodLists
    {
        public static IList<FoodDTO> ListOfFoodsIdStringsToFoodList(List<string> stringList,
            List<FoodDTO> foodsListFromApi)
        {
            if (stringList == null || foodsListFromApi == null || stringList.Count<=0)
            {
                throw new FormatException("Nothing to add or compare to");
            }
            var foods = new List<FoodDTO>();
            foreach (var item in stringList)
                if (int.TryParse(item, out var tempId))
                {
                    var food = foodsListFromApi.Find(x => x.Id == tempId);
                    foods.Add(food);
                }
                else
                {
                    throw new FormatException("Fail to add one item to list");
                }

            return foods;
        }
    }
}