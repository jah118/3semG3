using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.DataTransferObjects
{
    public class AvailableTimesDTO
    {
        public DateTime AvailabilityDate { get; set; }

        public IEnumerable<TableTimes> TableOpenings { get; set; }

        public class TableTimes
        {
            public RestaurantTablesDTO Table { get; set; }

            public IEnumerable<(TimeSpan start, TimeSpan end)> Openings { get; set; }
        }

    }
}


