using System;
using System.Collections.Generic;

namespace DataAccess.DataTransferObjects
{
    public class RestaurantTablesDTO
    {
        public RestaurantTablesDTO(int id, int noOfSeats, int tableNumber)
        {
            Id = id;
            NoOfSeats = noOfSeats;
            TableNumber = tableNumber;
        }

        public int Id { get; set; }
        public int NoOfSeats { get; set; }
        public int TableNumber { get; set; }
    }
}
