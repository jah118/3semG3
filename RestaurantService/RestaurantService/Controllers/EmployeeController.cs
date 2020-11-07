using DataAccess;
using DataAccess.DataTransferObjects;
using RestaurantService.Models;
using System.Web.Http;

namespace RestaurantService.Controllers
{
    public class EmployeeController : ApiController
    {
        private readonly IRepository<EmployeeDTO> _employeeRepository;

        public EmployeeController(IRepository<EmployeeDTO> employeeRepository)
        {
            _employeeRepository = employeeRepository;
        }

        // GET: api/Customer
        public IHttpActionResult Get()
        {
            return Ok(_employeeRepository.GetAll());
        }

        // GET: api/Customer/5
        public IHttpActionResult Get(int id)
        {
            return Ok(_employeeRepository.GetById(id));
        }

        // POST: api/Customer
        public IHttpActionResult Post([FromBody] EmployeeDTO employee)
        {
            return Ok(_employeeRepository.Create(employee));
        }

        // PUT: api/Customer/5
        public IHttpActionResult Put(int id, [FromBody] EmployeeDTO employee)
        {
            return Ok(_employeeRepository.Update(employee));
        }

        // DELETE: api/Customer/5
        public IHttpActionResult Delete(int id)
        {
            return Ok(); //TODO_employeeRepository.Delete(employee) //TODO
        }
    }
}