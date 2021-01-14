using System;
using System.Collections.Generic;
using RestaurantWebApp.DataTransferObject;

namespace RestaurantWebApp.Service.Interfaces
{
    public interface IReservationService : IService<ReservationDTO>
    {
        AvailableTimesDTO GetReservationTimeByDate(DateTime dateTime);
        IEnumerable<ReservationDTO> GetReservationByCustomerId();
    }
}