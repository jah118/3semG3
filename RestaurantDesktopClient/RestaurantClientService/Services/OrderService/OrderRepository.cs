﻿using Newtonsoft.Json;
using RestaurantClientService.DataTransferObjects;
using RestSharp;
using System.Collections.Generic;

namespace RestaurantClientService.Services.OrderService
{
    public class OrderRepository : IRepository<OrderDTO>
    {
        private readonly string _constring;

        public OrderRepository(string constring)
        {
            _constring = constring;
        }

        public OrderDTO Create(OrderDTO order)
        {
            OrderDTO res = null;
            try
            {
                var client = new RestClient(_constring);
                string json = JsonConvert.SerializeObject(order);
                var request = new RestRequest("/order", Method.POST);
                request.AddJsonBody(json);
                var response = client.Execute(request).Content;
                res = JsonConvert.DeserializeObject<OrderDTO>(response);
            }
            catch
            { }
            return res;
        }

        public IEnumerable<OrderDTO> GetAll()
        {
            IEnumerable<OrderDTO> res = new List<OrderDTO>();
            try
            {
                var client = new RestClient(_constring);
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
                var client = new RestClient(_constring);
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