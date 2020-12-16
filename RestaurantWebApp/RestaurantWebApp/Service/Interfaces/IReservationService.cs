using System;
using RestaurantWebApp.DataTransferObject;

namespace RestaurantWebApp.Service.Interfaces
{
    public interface IReservationService : IService<ReservationDTO>
    {
         AvailableTimesDTO GetReservationTimeByDate(DateTime dateTime);
    }
}