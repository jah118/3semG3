using Newtonsoft.Json;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Net;
using RestaurantDesktopClient.DataTransferObject;

namespace RestaurantDesktopClient.Services.Table_Service
{
    class TableRepository : ITableRepository
    {
        private readonly IAuthRepository _authRepository;
        private readonly string _constring;

        public TableRepository(string constring, IAuthRepository authRepository)
        {
            _authRepository = authRepository;
            this._constring = constring;
        }

        public IEnumerable<TablesDTO> GetAll()
        {
                var client = new RestClient(_constring);

                var request = new RestRequest("/Table", Method.GET);

                var response = client.Execute(request);

                return response.StatusCode== HttpStatusCode.OK ?
                    JsonConvert.DeserializeObject<List<TablesDTO>>(response.Content) :
                    new List<TablesDTO>();
        }

        public TablesDTO Get(int number)
        {
                var client = new RestClient(_constring);

                var request = new RestRequest("Tables/{Id}", Method.GET);
                request.AddUrlSegment("Id", number);

                var content = client.Execute(request).Content;

                return JsonConvert.DeserializeObject<TablesDTO>(content);
        }

        public TablesDTO Create(TablesDTO t)
        {
            throw new NotImplementedException();
        }

        public List<TablesDTO> GetFreeTables(DateTime date)
        {
                var constring = ConfigurationManager.ConnectionStrings["ServiceConString"].ConnectionString;
                var client = new RestClient(constring);
                var request = new RestRequest("/Table/OpenTables/{date}", Method.GET);
                request.AddUrlSegment("date", date.ToString("MM-dd-yy HH:mm:ss")); 
                var response = client.Execute(request);
                return response.StatusCode == HttpStatusCode.OK ? 
                    JsonConvert.DeserializeObject<List<TablesDTO>>(response.Content) : new List<TablesDTO>();

        }

        public TablesDTO Update(TablesDTO t)
        {
            throw new NotImplementedException();
        }

        public HttpStatusCode Delete(TablesDTO t)
        {
            throw new NotImplementedException();
        }
    }
}
