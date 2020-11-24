using DataAccess.DataTransferObjects;
using Newtonsoft.Json;
using RestSharp;
using System.Collections.Generic;
using System.Configuration;

namespace RestaurantDesktopClient.Views.ViewModels
{
    internal class FoodRepository : IFoodRepository
    {
        public FoodRepository()
        {

        }
        public List<FoodDTO> GetAllFoods()
        {
            List<FoodDTO> res = new List<FoodDTO>();
            try
            {
                //TODO: autofac readup and write..
                string constring = ConfigurationManager.ConnectionStrings["ServiceConString"].ConnectionString;
                var client = new RestClient(constring);
                var request = new RestRequest("/foods", Method.GET);
                var content = client.Execute(request).Content;
                res = JsonConvert.DeserializeObject<List<FoodDTO>>(content);
            }
            catch
            {
            }
            return (List<FoodDTO>)res;
        }


    }
}