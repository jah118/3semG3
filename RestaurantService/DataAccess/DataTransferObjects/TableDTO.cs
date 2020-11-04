using DataAccess.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.DataTransferObjects
{
   public class TableDTO
    {
        public int Id { get; }
        public int NoOfSeats { get; set; }
        public int TableNumber { get; set; }

        public virtual ICollection<ReservationsTables> ReservationsTables { get; set; }
    }
}
