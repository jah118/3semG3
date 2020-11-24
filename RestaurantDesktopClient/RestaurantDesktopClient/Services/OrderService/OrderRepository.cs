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

namespace RestaurantDesktopClient.Services.OrderService
{
    class OrderRepository : IRepository<OrderDTO>
    {
        public OrderRepository()
        {

        }
        public OrderDTO Create(OrderDTO order)
        {
            OrderDTO res = null;
            try
            {
                string constring = ConfigurationManager.ConnectionStrings["ServiceConString"].ConnectionString;
                var client = new RestClient(constring);
                string json = JsonConvert.SerializeObject(order);
                var request = new RestRequest("/order", Method.POST);
                request.AddJsonBody(json);
                var response = client.Execute(request).Content;
                res = JsonConvert.DeserializeObject<OrderDTO>(response);


            }
            catch
            {

            }
            return res;
        }

        public IEnumerable<OrderDTO> GetAll()
        {
            IEnumerable<OrderDTO> res = new List<OrderDTO>();
            try
            {
                string constring = ConfigurationManager.ConnectionStrings["ServiceConString"].ConnectionString;
                var client = new RestClient(constring);
                var request = new RestRequest("/order", Method.GET);
                var response = client.Execute(request).Content;
                res = JsonConvert.DeserializeObject<IEnumerable<OrderDTO>>(response);
            }
            catch
            {
            }
            return res;
        }

        public OrderDTO Get(int id)
        {
            OrderDTO res = null;
            try
            {
                string constring = ConfigurationManager.ConnectionStrings["ServiceConString"].ConnectionString;
                var client = new RestClient(constring);
                var request = new RestRequest("/order/{Id}", Method.GET);
                request.AddUrlSegment("Id", id);
                var response = client.Execute(request).Content;
                res = JsonConvert.DeserializeObject<OrderDTO>(response);
            }
            catch
            {
            }
            return res;
        }

    }
}
