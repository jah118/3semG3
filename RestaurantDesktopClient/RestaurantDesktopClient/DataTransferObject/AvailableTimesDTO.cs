using System;
using System.Collections.Generic;

namespace RestaurantDesktopClient.DataTransferObject
{
    public class AvailableTimesDTO
    {
        public DateTime AvailabilityDate { get; set; }

        public IEnumerable<TableTimes> TableOpenings { get; set; }

        public class TableTimes
        {
            public TablesDTO Table { get; set; }

            public IEnumerable<TimePair> Openings { get; set; }

            public class TimePair
            {
                public DateTime Start { get; set; }
                public DateTime End { get; set; }
            }
        }
    }
}
