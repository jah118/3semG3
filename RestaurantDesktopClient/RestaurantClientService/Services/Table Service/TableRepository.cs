using Newtonsoft.Json;
using RestaurantClientService.DataTransferObjects;
using RestSharp;
using System;
using System.Collections.Generic;

namespace RestaurantClientService.Services.Table_Service
{
    public class TableRepository : IRepository<TablesDTO>
    {
        private readonly string _constring;

        public TableRepository(string constring)
        {
            _constring = constring;
        }

        public IEnumerable<TablesDTO> GetAll()
        {
            List<TablesDTO> res = null;
            try
            {
                var client = new RestClient(_constring);

                var request = new RestRequest("/Table", Method.GET);

                var content = client.Execute(request).Content;

                res = JsonConvert.DeserializeObject<List<TablesDTO>>(content);
            }
            catch
            {
            }

            return res ?? new List<TablesDTO>();
        }

        public TablesDTO Get(int number)
        {
            TablesDTO res = null;
            try
            {
                var client = new RestClient(_constring);

                var request = new RestRequest("Tables/{Id}", Method.GET);
                request.AddUrlSegment("Id", number);

                var content = client.Execute(request).Content;

                res = (TablesDTO)JsonConvert.DeserializeObject(content);
            }
            catch
            {
            }
            return res;
        }

        public TablesDTO Create(TablesDTO t)
        {
            throw new NotImplementedException();
        }
    }
}