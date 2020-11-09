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
            var client = new RestClient("https://localhost:44349/api/reservation");

            var request = new RestRequest("Reservation", Method.GET);

            var content = client.Execute(request).Content;

            CustomerDTO res = (CustomerDTO)JsonConvert.DeserializeObject(content);
            res = new CustomerDTO { FirstName="jonna", LastName="jonnasen", Address="jonnavej", ZipCode="2131", City="Jonnaby", Email="JonnaMain" , Phone="12457896"}; //TODO remove when service is running

            return res;
        }
    }
}
