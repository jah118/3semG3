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
            var res = _reservationRepository.GetById(id);
            return res != null ? (IHttpActionResult)Ok(res) : Content(HttpStatusCode.NotFound, id);
        }

        // POST: api/Reservation
        public IHttpActionResult Post([FromBody]ReservationDTO value)
        {
            var res = _reservationRepository.Create(value);

            return res != null ? (IHttpActionResult)Ok(res) : Content(HttpStatusCode.Conflict, value);
        }
        
        // PUT: api/Reservation/5
        public IHttpActionResult Put(int id, [FromBody]ReservationDTO value)
        {
            return null;
        }

        // DELETE: api/Reservation/5
        public IHttpActionResult Delete(int id)
        {
            return null;
        }
    }
}
