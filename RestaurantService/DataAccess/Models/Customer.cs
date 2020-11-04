using System;
using System.Collections.Generic;

namespace DataAccess.Models
{
    public partial class Customer
    {
        public Customer()
        {
            Reservation = new HashSet<Reservation>();
        }

        public int Id { get; set; }
        public int? PersonId { get; set; }

        public virtual Person Person { get; set; }
        public virtual ICollection<Reservation> Reservation { get; set; }
    }
}
