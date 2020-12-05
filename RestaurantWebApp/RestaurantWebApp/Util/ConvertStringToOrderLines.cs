using RestaurantWebApp.DataTransferObject;
using System;
using System.Collections.Generic;
using System.Linq;

namespace RestaurantWebApp.Util
{
    public static class ConvertStringToOrderLines
    {
        public static IEnumerable<OrderLineDTO> ListOfFoodsIdToOrderLines(List<string> item3,
            List<FoodDTO> foodsListFromApi)
        {
            var orderLine = new List<OrderLineDTO>();
            var foods = new List<FoodDTO>();
            foreach (var item in item3)
            {
                if (int.TryParse(item, out var tempId))
                {
                    //var v=  new OrderLineDTO {1,new FoodDTO(2)};
                    var food = foodsListFromApi.Find(x => x.Id == tempId);

                    if (orderLine.Exists(o => o.Food.Id == tempId))
                    {
                        var obj = orderLine.Find(x => x.Food.Id == tempId);
                        obj.Quantity += 1;
                    }
                    else
                    {
                        orderLine.Add(new OrderLineDTO(1, food));
                    }
                }
                else
                {
                    throw new FormatException("Fail to add one item to list");
                }
            }

            return orderLine;
        }
    }
}