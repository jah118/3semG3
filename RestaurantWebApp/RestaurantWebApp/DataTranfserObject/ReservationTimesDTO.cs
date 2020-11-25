using System;

namespace DataTransferObjects
{
    public class ReservationTimesDTO
    {
        public ReservationTimesDTO()
        {
        }

        public ReservationTimesDTO(DateTime date, TimeSpan time)
        {
            _time = time;
            _date = date.Date + time; ;
        }

        public DateTime _date { get; set; }
        public TimeSpan _time { get; set; }
    }
}