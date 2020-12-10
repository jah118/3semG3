using System;
using System.Collections.Generic;
using DataAccess.DataTransferObjects;

namespace DataAccess.Repositories.Interfaces
{
    public interface ITableRepository : IRepository<RestaurantTablesDTO>
    {
        public List<RestaurantTablesDTO> GetOpenTablesByDateAndTime(DateTime dateTime);
        public AvailableTimesDTO GetReservationTimeByDate(DateTime dateTime);
    }
}