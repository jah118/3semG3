using DataAccess;
using DataAccess.Repositories;
using DataAccess.DataTransferObjects;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RestaurantAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [ApiConventionType(typeof(DefaultApiConventions))]
    public class OrderController : Controller
    {
        private IRepository<OrderDTO> _repository;
        public OrderController(IRepository<OrderDTO> repo)
        {
            _repository = repo;
        }
        [HttpGet]
        public IActionResult Get()
        {
            return Ok(_repository.GetAll());
        }

        [HttpGet("{id}")]
        public IActionResult Get(int id)
        {
            return Ok(_repository.GetById(id));
        }
    }
}
