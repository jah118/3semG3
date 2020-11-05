using DataAccess;
using DataAccess.DataTransferObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace RestaurantService.Controllers
{
    public class TableController : ApiController
    {
        private readonly IRepository<RestaurantTablesDTO> _tableRepository;

        public TableController(IRepository<RestaurantTablesDTO> tableRepository)
        {
            _tableRepository = tableRepository;
        }

        [HttpGet]
        public IHttpActionResult Get()
        {
            return Ok(_tableRepository.GetAll());
        }


    }
}
