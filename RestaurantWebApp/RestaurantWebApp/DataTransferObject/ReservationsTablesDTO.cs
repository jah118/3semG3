namespace RestaurantWebApp.DataTransferObject
{
    public class ReservationsTablesDTO
    {
        public int ReservationId { get; }
        public int RestaurantTablesId { get; }

        public ReservationDTO Reservation { get; }
        public  RestaurantTablesDTO RestaurantTables { get;  }
    }
}
