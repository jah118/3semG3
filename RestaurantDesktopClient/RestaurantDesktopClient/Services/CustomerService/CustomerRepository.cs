using DataAccess.DataTransferObjects;
using Newtonsoft.Json;
using RestaurantDesktopClient.Views.ViewModels;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace RestaurantDesktopClient.Services.CustomerService
{
    class CustomerRepository : IRepository<CustomerDTO>
    {
        private readonly string _constring;

        public CustomerRepository(string constring)
        {
            this._constring = constring;
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
            var response = client.Execute(request);

            CustomerDTO res = response.StatusCode == HttpStatusCode.OK ? JsonConvert.DeserializeObject<CustomerDTO>(response.Content) : null;

            return res;
        }

        public IEnumerable<CustomerDTO> GetAll()
        {
            throw new NotImplementedException();
        }
    }
}
