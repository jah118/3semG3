using System;
using System.Collections.Generic;
using Newtonsoft.Json;
using RestaurantClientService.DataTransferObjects;
using RestSharp;

namespace RestaurantClientService.Services.CustomerService
{
    class CustomerRepository : IRepository<CustomerDTO>
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
            string constring = ConfigurationManager.ConnectionStrings["ServiceConString"].ConnectionString;
            var client = new RestClient(constring);

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
