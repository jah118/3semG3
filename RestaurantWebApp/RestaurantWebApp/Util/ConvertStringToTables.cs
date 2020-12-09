using System;
using System.Collections.Generic;
using System.Linq;
using RestaurantWebApp.DataTransferObject;

namespace RestaurantWebApp.Util
{
    public static class ConvertStringToTables
    {
        //this method takes a string of this format "1,2,5,7"
        //and makes it to list of tables, where each table in the list has the id from the string
        public static IEnumerable<RestaurantTablesDTO> StringOfIdToTables(string value)
        {
            var listStrLineElements = value.Split(',').ToList();
            var tables = new List<RestaurantTablesDTO>();

            foreach (var item in listStrLineElements)
            {
                if (int.TryParse(item, out var tempId))
                    tables.Add(new RestaurantTablesDTO(tempId, 0, 0));

                else
                    throw new FormatException("Fail to add one item to list");
            }

            if (tables.Count <=0)
            {
                throw new FormatException("Fail no tables selected or formated");
            }

            return tables;
        }
    }

   


}