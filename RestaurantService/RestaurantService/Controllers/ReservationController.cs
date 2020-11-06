using DataAccess;
using DataAccess.DataTransferObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace RestaurantService.Controllers
{
    public class ReservationController : ApiController
    {
        private readonly IRepository<ReservationDTO> _reservationRepository;

        public ReservationController(IRepository<ReservationDTO> reservationRepository)
        {
            _reservationRepository = reservationRepository;
        }
        // GET: api/Reservation
        public IHttpActionResult Get()
        {
            return Ok(_reservationRepository.GetAll());
        }

        // GET: api/Reservation/5
        public IHttpActionResult Get(int id)
        {
            return Ok(_reservationRepository.GetById(id));
        }

        // POST: api/Reservation
        public IHttpActionResult Post([FromBody]string value)
        {

            
        }
        
        // PUT: api/Reservation/5
        public IHttpActionResult Put(int id, [FromBody]string value)
        {
        }

        // DELETE: api/Reservation/5
        public IHttpActionResult Delete(int id)
        {
        }
    }
}
