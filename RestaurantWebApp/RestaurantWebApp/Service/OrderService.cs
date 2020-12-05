using Newtonsoft.Json;
using RestaurantWebApp.DataTransferObject;
using RestaurantWebApp.Service.Interfaces;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using System.Web;

namespace RestaurantWebApp.Service
{
    public class OrderService : IOrderService
    {
        private string _conString;

        public OrderService(string configString)
        {
            this._conString = configString;
        }

        public OrderDTO Create(OrderDTO obj)
        {
            OrderDTO res;
            try
            {
                var client = new RestClient(_conString);
                string json = JsonConvert.SerializeObject(obj);
                var request = new RestRequest("/Order", Method.POST);
                request.AddJsonBody(json);
                var content = client.Execute(request).Content;
                res = JsonConvert.DeserializeObject<OrderDTO>(content);
            }
            catch
            {
                res = null;
            }


            return res;
        }
        //todo clean up and test
        public async Task<IRestResponse> CreateAsync(OrderDTO obj)
        {
            var client = new RestClient(_conString);
            //string json = JsonConvert.SerializeObject(obj);
            var request = new RestRequest("/Order", Method.POST);
            request.AddJsonBody(obj);
            //var content = client.Execute(request).Content;
            //res = JsonConvert.DeserializeObject<OrderDTO>(content);
            var response = (await client.ExecuteAsync(request));
            return response;
        }

        public bool Delete(OrderDTO obj)
        {
            throw new NotImplementedException();
        }

        public Task<bool> DeleteAsync(OrderDTO obj)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<OrderDTO> GetAll()
        {
            var client = new RestClient(_conString);
            var request = new RestRequest("/Order", Method.GET);
            var content = client.Execute(request).Content;
            var res = JsonConvert.DeserializeObject<IEnumerable<OrderDTO>>(content);
            return res;
        }

        public Task<IEnumerable<OrderDTO>> GetAllAsync()
        {
            throw new NotImplementedException();
        }

        public OrderDTO GetById(int id)
        {
            var client = new RestClient(_conString);
            var request = new RestRequest("/Order/{id}", Method.GET);
            request.AddParameter("id", id);
            var content = client.Execute(request).Content;
            var res = JsonConvert.DeserializeObject<OrderDTO>(content);
            return res;
        }

        public Task<OrderDTO> GetByIdAsync(int id)
        {
            throw new NotImplementedException();
        }

        public OrderDTO Update(OrderDTO obj)
        {
            throw new NotImplementedException();
        }

        public Task<OrderDTO> UpdateAsync(OrderDTO obj)
        {
            throw new NotImplementedException();
        }
    }
}