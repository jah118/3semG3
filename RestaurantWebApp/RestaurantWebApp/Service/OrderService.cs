using System;
using System.Collections.Generic;
using Newtonsoft.Json;
using RestaurantWebApp.DataTransferObject;
using RestaurantWebApp.Service.Interfaces;
using RestSharp;

namespace RestaurantWebApp.Service
{
    public class OrderService : IOrderService
    {
        private readonly IAuthService _authRepository;
        private readonly string _conString;

        public OrderService(string constring, IAuthService authRepository)
        {
            _conString = constring;
            _authRepository = authRepository;
        }

        public OrderDTO Create(OrderDTO obj)
        {
            var client = new RestClient(_conString);
            var request = new RestRequest("/Order", Method.POST);
            var json = JsonConvert.SerializeObject(obj);
            request.AddJsonBody(json);
            //_authRepository.AddTokenToRequest(request);
            var response = client.Execute(request).Content;
            var res = JsonConvert.DeserializeObject<OrderDTO>(response);
            return res;
        }

        public bool Delete(OrderDTO obj)
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

        public OrderDTO GetById(int id)
        {
            var client = new RestClient(_conString);
            var request = new RestRequest("/Order/{id}", Method.GET);
            request.AddParameter("id", id);
            var content = client.Execute(request).Content;
            var res = JsonConvert.DeserializeObject<OrderDTO>(content);
            return res;
        }

        public OrderDTO Update(OrderDTO obj)
        {
            throw new NotImplementedException();
        }
    }
}