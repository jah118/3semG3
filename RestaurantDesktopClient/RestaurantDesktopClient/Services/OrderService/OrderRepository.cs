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
            try
            {
                var client = new RestClient(_constring);
                string json = JsonConvert.SerializeObject(order);
                var request = new RestRequest("/order", Method.POST);
                request.AddJsonBody(json);
                var response = client.Execute(request).Content;
                return JsonConvert.DeserializeObject<OrderDTO>(response);
            }
            catch
            {
                return null;
            }
        }
        public IEnumerable<OrderDTO> GetAll()
        {
            try
            {
                IEnumerable<OrderDTO> res = null;
                var client = new RestClient(_constring);
                var request = new RestRequest("/order", Method.GET);
                if (_authRepository.AddTokenToRequest(request))
                {
                    var response = client.Execute(request).Content;
                    res = JsonConvert.DeserializeObject<IEnumerable<OrderDTO>>(response);
                }

                return res;
            }
            catch
            {
                return new List<OrderDTO>();
            }
        }
        //TODO: not used
        public OrderDTO Get(int id)
        {
            try
            {
                OrderDTO res = null;
                var client = new RestClient(_constring);
                var request = new RestRequest("/order/{Id}", Method.GET);
                request.AddUrlSegment("Id", id);
                if (_authRepository.AddTokenToRequest(request))
                {
                    var response = client.Execute(request).Content;
                    res = JsonConvert.DeserializeObject<OrderDTO>(response);
                }

                return res;
            }
            catch
            {
                return null;
            }
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
