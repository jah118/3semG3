using Newtonsoft.Json;
using RestaurantWebApp.DataTransferObject;
using RestaurantWebApp.Service.Interfaces;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace RestaurantWebApp.Service
{
    public class TableServices : ITableService
    {
        private readonly string _constring;

        public TableServices(string constring)
        {
            _constring = constring;
        }

        public IEnumerable<RestaurantTablesDTO> GetAll()
        {
            var client = new RestClient(_constring);
            var request = new RestRequest("/Table", Method.GET);
            var content = client.Execute(request).Content;
            var res = JsonConvert.DeserializeObject<IEnumerable<RestaurantTablesDTO>>(content);
            return res;
        }

        public RestaurantTablesDTO GetById(int id)
        {
            throw new NotImplementedException();
        }

        public bool Create(RestaurantTablesDTO obj)
        {
            throw new NotImplementedException();
        }

        public RestaurantTablesDTO Update(RestaurantTablesDTO obj)
        {
            throw new NotImplementedException();
        }

        public bool Delete(RestaurantTablesDTO obj)
        {
            throw new NotImplementedException();
        }

        public async Task<IEnumerable<RestaurantTablesDTO>> GetAllAsync()
        {
            var client = new RestClient(_constring);
            var request = new RestRequest("/Table", Method.GET);
            var content = (await client.ExecuteAsync(request)).Content;
            var res = JsonConvert.DeserializeObject<IEnumerable<RestaurantTablesDTO>>(content);
            return res;
        }

        public Task<RestaurantTablesDTO> GetByIdAsync(int id)
        {
            throw new NotImplementedException();
        }

        public Task<bool> CreateAsync(RestaurantTablesDTO obj)
        {
            throw new NotImplementedException();
        }

        public Task<RestaurantTablesDTO> UpdateAsync(RestaurantTablesDTO obj)
        {
            throw new NotImplementedException();
        }

        public Task<bool> DeleteAsync(RestaurantTablesDTO obj)
        {
            throw new NotImplementedException();
        }
    }
}