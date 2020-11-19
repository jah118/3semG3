using DataAccess;
using DataAccess.DataTransferObjects;
using Microsoft.AspNetCore.Mvc;

namespace RestaurantAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [ApiConventionType(typeof(DefaultApiConventions))]
    public class ReservationController : ControllerBase
    {
        private readonly IRepository<ReservationDTO> _reservationRepository;

        public ReservationController(IRepository<ReservationDTO> reservationRepository)
        {
            _reservationRepository = reservationRepository;
        }

        [HttpGet]
        public IActionResult Get()
        {
            var res = _reservationRepository.GetAll();
            return res != null ? Ok(res) : NotFound();
        }

        [HttpGet("{id}")]
        public IActionResult Get(int id)
        {
            var res = _reservationRepository.GetById(id);
            return res != null ? Ok(res) : NotFound(id);
        }

        [HttpPost]
        public IActionResult Post([FromBody] ReservationDTO value)
        {
            var res = _reservationRepository.Create(value);

            return res != null ? Ok(res) : Conflict(value);
        }

        [HttpPut]
        public IActionResult Put(int id, [FromBody] ReservationDTO value)
        {
            return NotFound();
        }

        [HttpDelete]
        public IActionResult Delete(int id)
        {
            return NotFound();
        }
    }
}