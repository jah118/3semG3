namespace DataAccess.Models
{
    public partial class ReservationsTables
    {
        public int ReservationId { get; set; }
        public int RestaurantTablesId { get; set; }

        public virtual Reservation Reservation { get; set; }
        public virtual RestaurantTables RestaurantTables { get; set; }
    }
}