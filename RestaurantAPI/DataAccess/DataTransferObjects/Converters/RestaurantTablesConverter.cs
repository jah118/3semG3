using DataAccess.Models;
using System.Collections.Generic;

namespace DataAccess.DataTransferObjects.Converters
{
    public partial class Converter
    {
        //Unsure if this makes more sense than disretely handling dto -> Model mapping, and having a secondary constructor
        public static RestaurantTables Convert(RestaurantTablesDTO obj)
        {
            return new RestaurantTables()
            {
                Id = obj.Id,
                NoOfSeats = obj.NoOfSeats,
                TableNumber = obj.TableNumber
            };
        }

        public static RestaurantTablesDTO Convert(RestaurantTables obj)
        {
            return new RestaurantTablesDTO(obj.Id)
            {
                Id = obj.Id,
                NoOfSeats = obj.NoOfSeats,
                TableNumber = obj.TableNumber
            };
        }

        public static IEnumerable<RestaurantTablesDTO> Convert(IEnumerable<RestaurantTables> obj)
        {
            var list = new List<RestaurantTablesDTO>();
            foreach (var t in obj)
            {
                list.Add(Convert(t));
            }
            return list;
        }
    }
}