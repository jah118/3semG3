using System;

namespace RestaurantWebApp.DataTransferObject
{
    public class ReservationTimesDTO
    {
        public ReservationTimesDTO()
        {
        }

        public ReservationTimesDTO(DateTime date, TimeSpan time)
        {
            Time = time;
            Date = date.Date + time;
        }

        public DateTime Date { get; set; }
        public TimeSpan Time { get; set; }
    }
}