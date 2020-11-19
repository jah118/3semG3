using DataAccess.DataTransferObjects;
using RestaurantDesktopClient.Reservation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RestaurantDesktopClient.Controllers
{
    class ReservationController
    {
        internal ReservationDTO GetReservationById(int id)
        {
            IReservationRepository ir = new ReservationRepository();
            ReservationDTO res = ir.GetReservation(id);
            return res;
        }
    }
}
