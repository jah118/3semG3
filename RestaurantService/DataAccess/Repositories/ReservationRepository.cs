using DataAccess.DataTransferObjects;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Repositories
{
    class ReservationRepository : IRepository<ReservationDTO>
    {

        private readonly RestaurantContext _context;

        public ReservationRepository(RestaurantContext context)
        {
            _context = context;
        }
        public ReservationDTO Create(ReservationDTO obj)
        {
            var transaction =_context.Database.BeginTransaction(IsolationLevel.Serializable);
            try
            {

            }
            catch(Exception)
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
