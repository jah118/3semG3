using Newtonsoft.Json;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Net;
using RestaurantDesktopClient.DataTransferObject;

namespace RestaurantDesktopClient.Services.CustomerService
{
    class CustomerRepository : IRepository<CustomerDTO>
    {
        private readonly string _constring;
        private readonly IAuthRepository _authRepository;

        public CustomerRepository(string constring, IAuthRepository authRepository)
        {
            this._constring = constring;
            _authRepository = authRepository;
        }

        public CustomerDTO Create(CustomerDTO t)
        {
            throw new NotImplementedException();
        }

        public HttpStatusCode Delete(CustomerDTO t)
        {
            throw new NotImplementedException();
        }

        public CustomerDTO Get(int customerId)
        {
                var client = new RestClient(_constring);
                var request = new RestRequest("/customer/{Id}", Method.GET);
                request.AddUrlSegment("Id", customerId);
                _authRepository.AddTokenToRequest(request);

                var response = client.Execute(request);

                return response.StatusCode == HttpStatusCode.OK
                    ? JsonConvert.DeserializeObject<CustomerDTO>(response.Content)
                    : null;
        }

        public IEnumerable<CustomerDTO> GetAll()
        {
            throw new NotImplementedException();
        }

        public CustomerDTO Update(CustomerDTO t)
        {
            throw new NotImplementedException();
        }
    }
}
