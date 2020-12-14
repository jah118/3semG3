using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows;
using DataAccess.DataTransferObjects;
using RestaurantDesktopClient.Services;

namespace RestaurantDesktopClient.Helpers
{
    public class Validation
    {
        public static bool IsNumber(string input, bool showMessage = true)
        {
            string regexPattern = @"^[0-9]+$";
            bool res = Regex.IsMatch(input, regexPattern);
            if (!res && showMessage) MessageBox.Show("Skal være et nummer");
            return res;
        }

        public static bool IsUpcomingDateTime(DateTime value, bool showMessage = true)
        {
            var res = value >= DateTime.Now;
            if (!res && showMessage) MessageBox.Show("Tidspunktet skal være efter nuværende tidspunkt");
            return res;
        }

        public static bool ReservationValidForCreate(ReservationDTO reservation, bool showMessage = true)
        {
            var res = true;
            var message = "Fejl i information: ";
            if (reservation.Tables.Count < 1)
            {
                message += "Ingen borde valgt, ";
                res = false;
            }
            if (!IsUpcomingDateTime(reservation.ReservationTime, false))
            {
                message += "Reservations tidspunkt er før nuværende tidspunkt, ";
                res = false;
            }
            if (reservation.Customer == null)
            {
                message += "Der er ikke valgt en kunde, ";
                res = false;
            }
            if (reservation.NoOfPeople < 1)
            {
                message += "Der skal minimum være 1 gæst, ";
                res = false;
            }

            if (!res && showMessage) MessageBox.Show(message.Remove(message.Length - 2, 2) + ".");
            return res;
        }

        public static bool ReservationValidForUpdate(ReservationDTO reservation, bool showMessage = true)
        {
            var res = true;
            var message = "Fejl i information: ";
            if (reservation.Id > 0)
            {
                if (reservation.Tables.Count < 1)
                {
                    message += "Ingen borde valgt, ";
                    res = false;
                }
                if (!IsUpcomingDateTime(reservation.ReservationTime, false))
                {
                    message += "Reservations tidspunkt er før nuværende tidspunkt, ";
                    res = false;
                }
                if (reservation.Customer == null)
                {
                    message += "Der er ikke valgt en kunde, ";
                    res = false;
                }
                if (reservation.NoOfPeople < 1)
                {
                    message += "Der skal minimum være 1 gæst, ";
                    res = false;
                }
            }
            else
            {
                res = false;
                message = "Ingen reservation valgt, ";
            }


            if (!res && showMessage) MessageBox.Show(message.Remove(message.Length - 2, 2) + ".");
            return res;
        }

        internal static bool ReservationValidForDelete(ReservationDTO reservation, 
            IRepository<OrderDTO> orderRepository, bool showMessage = true)
        {
            var res = true;
            var message = "Fejl i information: ";
            if (reservation.Id < 1)
            {
                res = false;
                message = "Ingen reservation valgt, ";
            }
            if(orderRepository.GetAll().Any(x => x.ReservationID == reservation.Id))
            {
                res = false;
                message = "Reservationen indholder en order og kan derfor ikke slettes, ";
            }


            if (!res && showMessage) MessageBox.Show(message.Remove(message.Length - 2, 2) + ".");
            return res;
        }
    }
}
