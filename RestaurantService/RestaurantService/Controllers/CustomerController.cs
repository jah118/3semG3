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
        private readonly IRepository<Customer2> _customerRepository;
        public CustomerController(IRepository<Customer2> customerRepository)
        {
            _customerRepository = customerRepository;
        }

        // GET: api/Customer
        public IHttpActionResult Get()
        {
            return null;
        }

        // GET: api/Customer/5
        public IHttpActionResult CustomerModel(int id)
        {
            return null;
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
