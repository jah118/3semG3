using System;
using System.Collections.Generic;

namespace DataAccess.DataAccess.Models
{
    public partial class RestaurantTables
    {
        public RestaurantTables()
        {
            ReservationsTables = new HashSet<ReservationsTables>();
        }

        public int Id { get; set; }
        public int NoOfSeats { get; set; }
        public int TableNumber { get; set; }

        public virtual ICollection<ReservationsTables> ReservationsTables { get; set; }
    }
}
