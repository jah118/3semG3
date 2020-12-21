using System.Collections.Generic;
using System.Net;
using Newtonsoft.Json;
using RestaurantDesktopClient.DataTransferObject;
using RestSharp;

namespace RestaurantDesktopClient.Services.FoodsService
{
    internal class FoodRepository : IRepository<FoodDTO>
    {
        private readonly string _constring;
        private readonly IAuthRepository _authRepository;

        public FoodRepository(string constring, IAuthRepository authRepository)
        {
            this._constring = constring;
            _authRepository = authRepository;
        }

        public FoodDTO Create(FoodDTO t)
        {
            throw new System.NotImplementedException();
        }

        public HttpStatusCode Delete(FoodDTO t)
        {
            throw new System.NotImplementedException();
        }

        public FoodDTO Get(int id)
        {
            throw new System.NotImplementedException();
        }

        public IEnumerable<FoodDTO> GetAll()
        {
                var client = new RestClient(_constring);
                var request = new RestRequest("/Food", Method.GET);
                var response = client.Execute(request);

                return response.StatusCode == HttpStatusCode.OK ? JsonConvert.DeserializeObject<List<FoodDTO>>(response.Content) : new List<FoodDTO>();
        }

        public FoodDTO Update(FoodDTO t)
        {
            throw new System.NotImplementedException();
        }
    }
}