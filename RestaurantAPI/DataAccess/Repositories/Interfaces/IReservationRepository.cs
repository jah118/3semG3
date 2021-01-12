using System;
using System.Collections.Generic;
using DataAccess.DataTransferObjects;

namespace DataAccess.Repositories.Interfaces
{
    public interface IReservationRepository : IRepository<ReservationDTO>
    {
        IEnumerable<ReservationDTO> ReservationsByCustomerUsername(string tokenUserName);
    }
}