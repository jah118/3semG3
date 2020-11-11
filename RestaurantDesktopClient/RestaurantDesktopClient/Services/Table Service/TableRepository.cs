using DataAccess.DataTransferObjects;
using Newtonsoft.Json;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RestaurantDesktopClient.Services.Table_Service
{
    class TableRepository : ITableRepository
    {
        public List<RestaurantTablesDTO> GetAllTables()
        {
            var client = new RestClient("https://localhost:44349/api");

            var request = new RestRequest("/Table", Method.GET);

            var content = client.Execute(request).Content;

            List<RestaurantTablesDTO> res = JsonConvert.DeserializeObject<List<RestaurantTablesDTO>>(content);

            return res;
        }

        public RestaurantTablesDTO getTableByNumber(int number)
        {
            var client = new RestClient("https://localhost:44349/api/tables");

            var request = new RestRequest("Tables/" + number, Method.GET);

            var content = client.Execute(request).Content;



            RestaurantTablesDTO res = (RestaurantTablesDTO)JsonConvert.DeserializeObject(content);

            res = new RestaurantTablesDTO { NoOfSeats = 4, TableNumber = 1 }; // TODO: remove when service running

            return res;
        }

    }
}
