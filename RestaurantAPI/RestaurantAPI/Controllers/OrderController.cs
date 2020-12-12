using System;
using DataAccess;
using DataAccess.DataTransferObjects;
using Microsoft.AspNetCore.Mvc;

namespace RestaurantAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [ApiConventionType(typeof(DefaultApiConventions))]
    public class OrderController : ControllerBase
    {
        private readonly IRepository<OrderDTO> _orderRepository;

        public OrderController(IRepository<OrderDTO> orderOrderRepository)
        {
            _orderRepository = orderOrderRepository;
        }

        [HttpGet]
        public IActionResult Get()
        {
            var res = _orderRepository.GetAll();
            return res != null ? (IActionResult)Ok(res) : NotFound();
        }

        [HttpGet("{id}")]
        public IActionResult Get(int id)
        {
            var res = _orderRepository.GetById(id);
            return res != null ? (IActionResult)Ok(res) : NotFound(id);
        }

        [HttpPut("{orderNo}")]
        public IActionResult Put(int orderNo, [FromBody] OrderDTO value)
        {
            if (orderNo == value.OrderNo)
            {
                try
                {
                    var res = _orderRepository.Update(value);
                    return res != null ? (IActionResult)Ok(res) : Conflict(value);
                }
                catch (Exception e)
                {
                    Console.WriteLine(e);
                    return BadRequest(value); 
                }
                
            }

            return BadRequest(value);
        }

        [HttpPost]
        public IActionResult Post([FromBody] OrderDTO value)
        {
            var res = _orderRepository.Create(value);
            return res != null ? (IActionResult)Ok(res) : Conflict(value);
        }
    }
}