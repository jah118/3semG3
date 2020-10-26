using DataAccess;
using DataAccess.Models;
using RestaurantService.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace RestaurantService.Controllers
{
    public class CustomerController : ApiController
    {
        private readonly IRepository<Customer> _customerRepository;
        public CustomerController(IRepository<Customer> customerRepository)
        {
            _customerRepository = customerRepository;
        }

        // GET: api/Customer
        public IEnumerable<CustomerModel> Get()
        {
            return null;
        }

        // GET: api/Customer/5
        public CustomerModel CustomerModel(int id)
        {
            return null;
        }

        // POST: api/Customer
        public void Post([FromBody] CustomerModel customer)
        {
        }

        // PUT: api/Customer/5
        public void Put(int id, [FromBody] CustomerModel customer)
        {
        }

        // DELETE: api/Customer/5
        public void Delete(int id)
        {
        }
    }
}
