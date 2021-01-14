using System;
using System.Collections.Generic;
using Newtonsoft.Json;
using RestaurantWebApp.DataTransferObject;
using RestaurantWebApp.Service.Interfaces;
using RestSharp;

namespace RestaurantWebApp.Service
{
    public class FoodService : IFoodService
    {
        private readonly string _constring;

        public FoodService(string constring)
        {
            _constring = constring;
        }

        public FoodDTO Create(FoodDTO obj)
        {
            throw new NotImplementedException();
        }

        public bool Delete(FoodDTO obj)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<FoodDTO> GetAll()
        {
            var client = new RestClient(_constring);
            var request = new RestRequest("/Food", Method.GET);
            var content = client.Execute(request).Content;
            var res = JsonConvert.DeserializeObject<IEnumerable<FoodDTO>>(content);
            return res;
        }

        public FoodDTO GetById(int id)
        {
            throw new NotImplementedException();
        }

        public FoodDTO Update(FoodDTO obj)
        {
            throw new NotImplementedException();
        }
    }
}