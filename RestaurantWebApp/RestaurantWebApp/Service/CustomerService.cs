using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using System.Web;
using Newtonsoft.Json;
using RestaurantWebApp.DataTransferObject;
using RestaurantWebApp.Service.Interfaces;
using RestSharp;

namespace RestaurantWebApp.Service
{
    public class CustomerService : IService<CustomerDTO>
    {

        private readonly string _constring;
        private readonly IAuthService _authRepository;

        public CustomerService(string constring, IAuthService authRepository)
        {
            _constring = constring;
            _authRepository = authRepository;
        }

        //public CustomerService(string constring)
        //{
        //    _constring = constring;
        //}

        public CustomerDTO GetById(int id)
        {
            var client = new RestClient(_constring);
            var request = new RestRequest("/customer/{id}", Method.GET);
            request.AddUrlSegment("id", id);
            _authRepository.AddTokenToRequest(request);
            var response = client.Execute(request);

            return response.StatusCode == HttpStatusCode.OK
                ? JsonConvert.DeserializeObject<CustomerDTO>(response.Content)
                : null;
        }

        public CustomerDTO Create(CustomerDTO t)
        {
            throw new NotImplementedException();
        }

        public HttpStatusCode Delete(CustomerDTO t)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<CustomerDTO>> GetAllAsync()
        {
            throw new NotImplementedException();
        }

        public Task<CustomerDTO> GetByIdAsync(int id)
        {
            throw new NotImplementedException();
        }

        public Task<IRestResponse> CreateAsync(CustomerDTO obj)
        {
            throw new NotImplementedException();
        }

        public Task<CustomerDTO> UpdateAsync(CustomerDTO obj)
        {
            throw new NotImplementedException();
        }

        public Task<bool> DeleteAsync(CustomerDTO obj)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<CustomerDTO> GetAll()
        {
            throw new NotImplementedException();
        }

        public CustomerDTO Update(CustomerDTO t)
        {
            throw new NotImplementedException();
        }

        bool IService<CustomerDTO>.Delete(CustomerDTO obj)
        {
            throw new NotImplementedException();
        }
    }


}