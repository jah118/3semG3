using System;
using System.Collections.Generic;
using DataAccess.DataTransferObjects;

namespace DataAccess.Repositories.Interfaces
{
    public interface IReservationRepository : IRepository<ReservationDTO>
    {
        public IEnumerable<ReservationTimeSlots> GetReservationTimeByDate(DateTime dateTime);
    }
}