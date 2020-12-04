using Newtonsoft.Json;
using RestaurantClientService.DataTransferObjects;
using RestSharp;
using System;
using System.Collections.Generic;

namespace RestaurantClientService.Services.CustomerService
{
    public class CustomerRepository : IRepository<CustomerDTO>
    {
        private readonly string _constring;

        public CustomerRepository(string constring)
        {
            _constring = constring;
        }

        public CustomerDTO Create(CustomerDTO t)
        {
            throw new NotImplementedException();
        }

        public CustomerDTO Get(int customerId)
        {
            var client = new RestClient(_constring);

            var request = new RestRequest("/customer/{Id}", Method.GET);
            request.AddUrlSegment("Id", customerId);
            var content = client.Execute(request).Content;

            CustomerDTO res = JsonConvert.DeserializeObject<CustomerDTO>(content);

            return res;
        }

        public IEnumerable<CustomerDTO> GetAll()
        {
            throw new NotImplementedException();
        }
    }
}