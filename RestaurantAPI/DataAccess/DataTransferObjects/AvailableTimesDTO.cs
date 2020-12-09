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

            public IEnumerable<TimePair> Openings { get; set; }

            public class TimePair
            {
                public DateTime Start { get; set; }
                public DateTime End { get; set; }
            }
        }

    }
}


