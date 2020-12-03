using DataAccess.DataTransferObjects;
using Newtonsoft.Json;
using RestaurantDesktopClient.Views.ViewModels;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RestaurantDesktopClient.Services.Table_Service
{
    class TableRepository : IRepository<TablesDTO>
    {
        public IEnumerable<TablesDTO> GetAll()
        {
            string constring = ConfigurationManager.ConnectionStrings["ServiceConString"].ConnectionString;
            var client = new RestClient(constring);

            var request = new RestRequest("/Table", Method.GET);

            var content = client.Execute(request).Content;

            List<TablesDTO> res = JsonConvert.DeserializeObject<List<TablesDTO>>(content);

            return res;
        }

        public TablesDTO Get(int number)
        {
            string constring = ConfigurationManager.ConnectionStrings["ServiceConString"].ConnectionString;
            var client = new RestClient(constring);

            var request = new RestRequest("Tables/{Id}", Method.GET);
            request.AddUrlSegment("Id", number);

            var content = client.Execute(request).Content;

            TablesDTO res = (TablesDTO)JsonConvert.DeserializeObject(content);

            return res;
        }

        public TablesDTO Create(TablesDTO t)
        {
            throw new NotImplementedException();
        }
    }
}
