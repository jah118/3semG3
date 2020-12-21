using Newtonsoft.Json;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Net;
using RestaurantDesktopClient.DataTransferObject;

namespace RestaurantDesktopClient.Services.OrderService
{
    class OrderRepository : IRepository<OrderDTO>
    {
        private readonly string _constring;
        private readonly IAuthRepository _authRepository;

        public OrderRepository(string constring, IAuthRepository authRepository)
        {
            this._constring = constring;
            _authRepository = authRepository;
        }

        public OrderDTO Create(OrderDTO order)
        {
            var client = new RestClient(_constring);
            string json = JsonConvert.SerializeObject(order);
            var request = new RestRequest("/order", Method.POST);
            request.AddJsonBody(json);
            var response = client.Execute(request);
            return response.StatusCode == HttpStatusCode.OK ? JsonConvert.DeserializeObject<OrderDTO>(response.Content) : null;
        }

        public IEnumerable<OrderDTO> GetAll()
        {
                IEnumerable<OrderDTO> res = null;
                var client = new RestClient(_constring);
                var request = new RestRequest("/order", Method.GET);

                if (_authRepository.AddTokenToRequest(request))
                {
                    var response = client.Execute(request);
                    res = response.StatusCode == HttpStatusCode.OK ? JsonConvert.DeserializeObject<IEnumerable<OrderDTO>>(response.Content) : new List<OrderDTO>();
                }

                return res;
        }

        public OrderDTO Get(int id)
        {
                OrderDTO res = null;
                var client = new RestClient(_constring);
                var request = new RestRequest("/order/{Id}", Method.GET);
                request.AddUrlSegment("Id", id);

                if (_authRepository.AddTokenToRequest(request))
                {
                    var response = client.Execute(request);
                    res = response.StatusCode == HttpStatusCode.OK ? JsonConvert.DeserializeObject<OrderDTO>(response.Content) : null;
                }

                return res;
        }

        public OrderDTO Update(OrderDTO t)
        {
            throw new NotImplementedException();
        }

        public HttpStatusCode Delete(OrderDTO t)
        {
            throw new NotImplementedException();
        }
    }
}
