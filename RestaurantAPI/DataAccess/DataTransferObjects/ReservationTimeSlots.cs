using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.DataTransferObjects
{
    public class ReservationTimeSlots
    {
        public DateTime DateTime { get; set; }

        public IList<RestaurantTablesDTO> FreeTables { get; set; }


    }
}
