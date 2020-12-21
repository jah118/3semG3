using DataAccess.Repositories.Interfaces;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using System;

namespace RestaurantAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [ApiConventionType(typeof(DefaultApiConventions))]
    public class TableController : ControllerBase
    {
        private readonly ITableRepository _tableRepository;

        public TableController(ITableRepository tableRepository)
        {
            _tableRepository = tableRepository;
        }

        [HttpGet]
        public IActionResult Get()
        {
            return Ok(_tableRepository.GetAll());
        }

        [EnableCors("PublicApi")]
        [HttpGet("OpenTables/{date}")]
        public IActionResult GetOpenTables(DateTime date)
        {
            var response = _tableRepository.GetOpenTablesByDateAndTime(date);
            if (response == null) return BadRequest("Requested time outside opening hours");
            return Ok(response);
        }

        [EnableCors("PublicApi")]
        [HttpGet("timeSlot/{date}")]
        public IActionResult Get(DateTime date)
        {
            var res = _tableRepository.GetReservationTimeByDate(date);
            return res != null ? (IActionResult)Ok(res) : NotFound(date);
        }
    }
}