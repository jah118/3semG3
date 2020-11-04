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
        public IHttpActionResult Customer(int id)
        {
            return Ok(_customerRepository.GetById(id));
        }

        // POST: api/Customer
        public IHttpActionResult Post([FromBody] CustomerModel customer)
        {
            return null;
        }

        // PUT: api/Customer/5
        public IHttpActionResult Put(int id, [FromBody] CustomerModel customer)
        {
            return null;
        }

        // DELETE: api/Customer/5
        public IHttpActionResult Delete(int id)
        {
            return null;
        }
    }
}