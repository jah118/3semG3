using System;
using Microsoft.Ajax.Utilities;

namespace RestaurantWebApp.Util
{
    public static class FormatTime
    {
        //if the date string contains a time element hh:mm:ss,
        //[1]then the string is split so only date remains so a new time can be added
        //[2]else just add time to date
        public static DateTime FormatterForReservationTimeFromString(string date, string timeStamp)
        {
            var timeSplit = timeStamp.Split(' ');

            if (date.Length < 4)
            {
                throw new FormatException("No date, cant format without a date");
            }
            //[1]
            if (date.Contains(" "))
            {
                var dateSplit = date.Split(' ');
                var temp = dateSplit[0] + " " + timeSplit[1];
                DateTime.TryParse(temp, out var datetime);
                return datetime;
            }
            //[2]
            else
            {
                var temp = date + " " + timeSplit[1];
                DateTime.TryParse(temp, out var datetime);
                return datetime;
            }
        }
    }
}