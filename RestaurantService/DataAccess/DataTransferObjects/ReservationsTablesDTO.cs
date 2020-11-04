using System;
using System.Collections.Generic;

namespace DataAccess.DataTransferObjects
{
    public class ReservationsTablesDTO
    {
        public int ReservationId { get; }
        public int RestaurantTablesId { get; set; }

        public ReservationDTO Reservation { get; set; }
        public ICollection<ReservationsTablesDTO> ReservationsTables { get; set; }
    }
}
