using DataAccess;
using DataAccess.DataTransferObjects;
using RestaurantService.Models;
using System.Web.Http;

namespace RestaurantService.Controllers
{
    public class CustomerController : ApiController
    {
        private readonly IRepository<CustomerDTO> _customerRepository;

        public CustomerController(IRepository<CustomerDTO> customerRepository)
        {
            _customerRepository = customerRepository;
        }

        // GET: api/Customer
        public IHttpActionResult Get()
        {
            return Ok(_customerRepository.GetAll());
        }

        // GET: api/Customer/5
        public IHttpActionResult Get(int id)   //Customer --> Get
        {
            var customer =_customerRepository.GetById(id);
            if (customer == null)
            {
                return NotFound();
            }
            return Ok();
        }

        // POST: api/Customer
        public IHttpActionResult Post([FromBody] CustomerDTO customer)
        {
            return Ok(_customerRepository.Create(customer));
        }

        // PUT: api/Customer/5
        public IHttpActionResult Put(int id, [FromBody] CustomerDTO customer)
        {
            return Ok(_customerRepository.Update(customer));
        }

        // DELETE: api/Customer/5
        public IHttpActionResult Delete(int id)
        {
            return Ok("not implemnete´t");
        }
    }
}