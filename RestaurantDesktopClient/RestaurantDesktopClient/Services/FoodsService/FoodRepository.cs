using DataAccess.DataTransferObjects;
using Newtonsoft.Json;
using RestSharp;
using System.Collections.Generic;
using System.Configuration;

namespace RestaurantDesktopClient.Views.ViewModels
{
    internal class FoodRepository : IRepository<FoodDTO>
    {
        private readonly string _constring;

        public FoodRepository()
        {

        }

        public FoodRepository(string constring)
        {
            this._constring = constring;
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