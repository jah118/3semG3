using DataAccess.DataTransferObjects;
using Newtonsoft.Json;
using RestSharp;
using System.Collections.Generic;
using System.Configuration;

namespace RestaurantDesktopClient.Views.ViewModels
{
    internal class FoodRepository : IRepository<FoodDTO>
    {
        public FoodRepository()
        {

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
            List<FoodDTO> res = new List<FoodDTO>();
            try
            {
                //TODO: autofac readup and write..
                string constring = ConfigurationManager.ConnectionStrings["ServiceConString"].ConnectionString;
                var client = new RestClient(constring);
                var request = new RestRequest("/Food", Method.GET);
                var content = client.Execute(request).Content;
                res = JsonConvert.DeserializeObject<List<FoodDTO>>(content);
            }
            catch
            {
            }
            return res;
        }
    }
}