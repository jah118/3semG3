using System;
using System.Collections.Generic;

namespace DataAccess.DataTransferObjects
{
    public class ReservationsTablesDTO
    {
        public int ReservationId { get; }
        public int RestaurantTablesId { get; }

        public ReservationDTO Reservation { get; }
        public  RestaurantTablesDTO RestaurantTables { get;  }
    }
}
