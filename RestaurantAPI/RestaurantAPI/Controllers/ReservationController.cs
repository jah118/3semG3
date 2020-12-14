using DataAccess.DataTransferObjects;
using DataAccess.Repositories.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace RestaurantAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [ApiConventionType(typeof(DefaultApiConventions))]
    public class ReservationController : ControllerBase
    {
        private readonly IReservationRepository _reservationRepository;

        public ReservationController(IReservationRepository reservationRepository)
        {
            _reservationRepository = reservationRepository;
        }

        [Authorize(Roles = "Employee")]
        [HttpGet]
        public IActionResult Get()
        {
            var res = _reservationRepository.GetAll();
            return res != null ? (IActionResult)Ok(res) : NotFound();
        }

        [Authorize]
        [HttpGet("{id}")]
        public IActionResult Get(int id)
        {
            var res = _reservationRepository.GetById(id);
            return res != null ? (IActionResult)Ok(res) : NotFound(id);
        }

        [HttpPost]
        public IActionResult Post([FromBody] ReservationDTO value)
        {
            var res = _reservationRepository.Create(value);
            return res != null ? (IActionResult)Ok(res) : Conflict(value);
        }

        [Authorize]
        [HttpPut("{id}")]
        public IActionResult Put(int id, [FromBody] ReservationDTO value)
        {
            if (id == value.Id)
            {
                var res = _reservationRepository.Update(value);
                return res != null ? (IActionResult)Ok(res) : Conflict(value);
            }

            return BadRequest(value);
        }

        [Authorize]
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            var res = _reservationRepository.Delete(id);
            return res != false ? (IActionResult)Ok(res) : Conflict(id);
        }
    }
}