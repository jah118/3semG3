using Newtonsoft.Json;
using RestaurantClientService.DataTransferObjects;
using RestSharp;
using System.Collections.Generic;

namespace RestaurantClientService.Services.FoodsService
{
    public class FoodRepository : IRepository<FoodDTO>
    {
        private readonly string _constring;

        public FoodRepository(string constring)
        {
            _constring = constring;
        }

        public FoodDTO Create(FoodDTO t)
        {
            throw new System.NotImplementedException();
        }

        public FoodDTO Get(int id)
        {
            throw new System.NotImplementedException();
        }

        public IEnumerable<FoodDTO> GetAll()
        {
            List<FoodDTO> res = null;
            try
            {
                var client = new RestClient(_constring);
                var request = new RestRequest("/Food", Method.GET);
                var content = client.Execute(request).Content;
                res = JsonConvert.DeserializeObject<List<FoodDTO>>(content);
            }
            catch
            {
            }
            return res ?? new List<FoodDTO>();
        }
    }
}