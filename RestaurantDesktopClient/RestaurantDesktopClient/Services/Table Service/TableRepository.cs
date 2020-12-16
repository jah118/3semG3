using DataAccess.DataTransferObjects;
using Newtonsoft.Json;
using RestaurantDesktopClient.Views.ViewModels;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Web.UI.WebControls;
using System.Windows;
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
        //TODO not used
        public IEnumerable<TablesDTO> GetAll()
        {
            try
            {
                var client = new RestClient(_constring);

                var request = new RestRequest("/Table", Method.GET);

                var content = client.Execute(request).Content;

                return JsonConvert.DeserializeObject<List<TablesDTO>>(content);
            }
            catch
            {
                return new List<TablesDTO>();
            }
        }
        //TODO not Used
        public TablesDTO Get(int number)
        {
            try
            {
                var client = new RestClient(_constring);

                var request = new RestRequest("Tables/{Id}", Method.GET);
                request.AddUrlSegment("Id", number);

                var content = client.Execute(request).Content;

                return JsonConvert.DeserializeObject<TablesDTO>(content);
            }
            catch
            {
                return null;
            }
        }

        public TablesDTO Create(TablesDTO t)
        {
            throw new NotImplementedException();
        }
        public List<TablesDTO> GetFreeTables(DateTime date)
        {
            try
            {
                var constring = ConfigurationManager.ConnectionStrings["ServiceConString"].ConnectionString;
                var client = new RestClient(constring);
                var request = new RestRequest("/Table/OpenTables/{date}", Method.GET);
                request.AddUrlSegment("date", date.ToString("MM-dd-yy HH:mm:ss")); 
                var response = client.Execute(request);
                return response.StatusCode == HttpStatusCode.OK ? JsonConvert.DeserializeObject<List<TablesDTO>>(response.Content) : new List<TablesDTO>();
            }
            catch
            {
                return new List<TablesDTO>();
            }
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
