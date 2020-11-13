using DataAccess.DataTransferObjects;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace RestaurantDesktopClient.Reservation
{
    public interface IReservationRepository
    {
        List<ReservationDTO> GetAllReservations();
        System.Net.HttpStatusCode CreateReservation(ReservationDTO reservation);
        ReservationDTO GetReservation(int id);
    }
}
