using System;
using System.Collections.Generic;

namespace DataAccess.Models
{
    public partial class Reservation
    {
        public Reservation()
        {
            ReservationsTables = new HashSet<ReservationsTables>();
            RestaurantOrder = new HashSet<RestaurantOrder>();
        }

        public int Id { get; set; }
        public DateTime ReservationDate { get; set; }
        public int CustomerId { get; set; }
        public DateTime ReservationTime { get; set; }
        public int NoOfPeople { get; set; }
        public bool? Deposit { get; set; }
        public string Note { get; set; }

        public virtual Customer Customer { get; set; }
        public virtual ICollection<ReservationsTables> ReservationsTables { get; set; }
        public virtual ICollection<RestaurantOrder> RestaurantOrder { get; set; }
    }
}
