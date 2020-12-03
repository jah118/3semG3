using DataAccess;
using DataAccess.DataTransferObjects;
using Microsoft.AspNetCore.Mvc;

namespace RestaurantAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [ApiConventionType(typeof(DefaultApiConventions))]
    public class TableController : ControllerBase
    {
        private readonly IRepository<RestaurantTablesDTO> _tableRepository;

        public TableController(IRepository<RestaurantTablesDTO> tableRepository)
        {
            _tableRepository = tableRepository;
        }

        [HttpGet]
        public IActionResult Get()
        {
            return Ok(_tableRepository.GetAll());
        }
    }
}