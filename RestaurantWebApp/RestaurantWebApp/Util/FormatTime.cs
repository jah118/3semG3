using System;

namespace RestaurantWebApp.Util
{
    public static class FormatTime
    {
        public static DateTime? FormatterForReservationTimeFromString(string date, string timeStamp)
        {
            var timeSplit = timeStamp.Split(' ');

            if (date.Contains(" "))
            {
                var dateSplit = date.Split(' ');
                var temp = dateSplit[0] + " " + timeSplit[1];
                DateTime.TryParse(temp, out var datetime);
                return datetime;
            }
            else
            {
                var temp = date + " " + timeSplit[1];
                DateTime.TryParse(temp, out var datetime);
                return datetime;
            }
        }
    }
}