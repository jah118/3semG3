using RestaurantWebApp.DataTransferObject;
using System;

namespace RestaurantWebApp.Service.Interfaces
{
    public interface IReservationService : IService<ReservationDTO>
    {
        AvailableTimesDTO GetReservationTimeByDate(DateTime dateTime);
    }
}