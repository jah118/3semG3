using DataAccess.DataTransferObjects;
using Newtonsoft.Json;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RestaurantDesktopClient.Services.CustomerService
{
    class CustomerRepository : ICustomerRepository
    {
        public CustomerDTO GetCustomerById(int customerId)
        {
            var client = new RestClient("https://localhost:44349/api");

            var request = new RestRequest("/customer/{Id}", Method.GET);
            request.AddUrlSegment("Id", customerId);
            var content = client.Execute(request).Content;

            CustomerDTO res = JsonConvert.DeserializeObject<CustomerDTO>(content);

            return res;
        }
    }
}
