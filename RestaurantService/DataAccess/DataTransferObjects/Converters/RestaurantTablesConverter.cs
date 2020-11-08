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
            throw new NotImplementedException();
        }

        public static RestaurantTablesDTO Convert(RestaurantTables obj)
        {
            throw new NotImplementedException();
        }
    }
}
