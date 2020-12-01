using DataAccess.DataTransferObjects;
using Newtonsoft.Json;
using RestaurantDesktopClient.Views.ViewModels;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RestaurantDesktopClient.Services.CustomerService
{
    class CustomerRepository : IRepository<CustomerDTO>
    {
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
