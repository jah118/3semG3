﻿namespace DataAccess.DataTransferObjects
{
    public class RestaurantTablesDTO
    {
        public RestaurantTablesDTO()
        {
        }

        public RestaurantTablesDTO(int id)
        {
            Id = id;
        }

        public int Id { get; }
        public int NoOfSeats { get; set; }
        public int TableNumber { get; set; }
    }
}