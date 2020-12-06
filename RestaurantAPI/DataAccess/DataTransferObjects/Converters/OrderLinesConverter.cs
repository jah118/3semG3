using DataAccess.Models;
using System.Collections.Generic;

namespace DataAccess.DataTransferObjects.Converters
{
    public partial class Converter
    {
        //Unsure if this makes more sense than disretely handling dto -> Model mapping, and having a secondary constructor
        public static OrderLine Convert(OrderLineDTO obj)
        {
            return new OrderLine()
            {
                Quantity = obj.Quantity,
                Food = Converter.Convert(obj.Food)
            };
        }

        public static OrderLineDTO Convert(OrderLine obj)
        {
            return new OrderLineDTO()
            {
                Quantity = obj.Quantity,
                Food = Converter.Convert(obj.Food)
            };
        }


        public static IEnumerable<OrderLine> Convert(IEnumerable<OrderLineDTO> obj)
        {
            var list = new List<OrderLine>();
            foreach (var t in obj)
            {
                list.Add(Convert(t));
            }
            return list;
        }

        public static IEnumerable<OrderLineDTO> Convert(IEnumerable<OrderLine> obj)
        {
            var list = new List<OrderLineDTO>();
            foreach (var t in obj)
            {
                list.Add(Convert(t));
            }
            return list;
        }
    }
}