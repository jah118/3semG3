using System;
using System.Collections.Generic;

namespace DataAccess.DataTransferObjects
{
    public partial class RestaurantTablesDTO
    {
        public RestaurantTablesDTO()
        {

        }

        public int Id { get; }
        public int NoOfSeats { get; set; }
        public int TableNumber { get; set; }

       
    }
}
