using RestaurantWebApp.DataTransferObject;
using System;
using System.Collections.Generic;

namespace RestaurantWebApp.Service.Interfaces
{
    public interface IReservationService : IService<ReservationDTO>
    {
        AvailableTimesDTO GetReservationTimeByDate(DateTime dateTime);
        IEnumerable<ReservationDTO> GetReservationByCustomerId(int id);

    }
}