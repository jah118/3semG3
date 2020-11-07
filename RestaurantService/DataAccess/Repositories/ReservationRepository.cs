using DataAccess.DataTransferObjects;
using DataAccess.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;

namespace DataAccess.Repositories
{
    internal class ReservationRepository : IRepository<ReservationDTO>
    {
        private readonly RestaurantContext _context;

        public ReservationRepository(RestaurantContext context)
        {
            _context = context;
        }

        public ReservationDTO Create(ReservationDTO obj)
        {
            var transaction = _context.Database.BeginTransaction(IsolationLevel.Serializable);
            try
            {
                //_context.ReservationsTables.Include(r => r.Reservation).Where(r => r.RestaurantTables.Id == obj.)

                //_context.Reservation.Where(r =>
                //r.ReservationTime <= obj.ReservationTime.AddHours(1).AddMinutes(30) &&
                //r.ReservationTime.AddHours(1).AddMinutes(30) > obj.ReservationTime).Count();
            }
            catch (Exception)
            {
                transaction.Rollback();
            }

            throw new NotImplementedException();
        }

        public bool Delete(ReservationDTO obj)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<ReservationDTO> GetAll()
        {
            throw new NotImplementedException();
        }

        public ReservationDTO GetById(int id)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<ReservationDTO> GetCountWithOffsetByOrdering(int count, int offset, string ordering)
        {
            throw new NotImplementedException();
        }

        public ReservationDTO Update(ReservationDTO obj)
        {
            throw new NotImplementedException();
        }
    }
}