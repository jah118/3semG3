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
        //Unsure if this makes more sense than disretely handling dto -> Model mapping, and having a secondary constructor
        public static RestaurantTables Convert(RestaurantTablesDTO obj)
        {
            return new RestaurantTables()
            {
                NoOfSeats = obj.NoOfSeats,
                TableNumber = obj.TableNumber
            };
        }

        public static RestaurantTablesDTO Convert(RestaurantTables obj)
        {
            return new RestaurantTablesDTO(obj.Id)
            {
                NoOfSeats = obj.NoOfSeats,
                TableNumber = obj.TableNumber
            };
        }

        public static IEnumerable<RestaurantTablesDTO> Convert(IEnumerable<RestaurantTables> obj)
        {
            var list = new List<RestaurantTablesDTO>();
            foreach(var t in obj)
            {
                list.Add(Convert(t));
            }
            return list;
        }
    }
}
