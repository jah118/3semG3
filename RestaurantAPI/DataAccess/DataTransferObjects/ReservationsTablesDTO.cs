using System;
using System.Collections.Generic;

namespace DataAccess.DataTransferObjects
{
    public class ReservationsTablesDTO
    {
        //DO NOT USE UNTIL WE ARE SURE WHETHER ITS NEEDED TODO DELETE
        public int ReservationId { get; }
        public int RestaurantTablesId { get; set; }

        public ReservationDTO Reservation { get; set; }
        public  RestaurantTablesDTO RestaurantTables { get; set; }
    }
}
