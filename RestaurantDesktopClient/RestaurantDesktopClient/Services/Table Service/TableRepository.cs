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
using System.Web.UI.WebControls;
using System.Windows;
using RestaurantDesktopClient.DataTransferObject;

namespace RestaurantDesktopClient.Services.Table_Service
{
    class TableRepository : ITableRepository<TablesDTO>
    {
        public IEnumerable<TablesDTO> GetAll()
        {
            List<TablesDTO> res = null;
            try
            {
                string constring = ConfigurationManager.ConnectionStrings["ServiceConString"].ConnectionString;
                var client = new RestClient(constring);

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
                string constring = ConfigurationManager.ConnectionStrings["ServiceConString"].ConnectionString;
                var client = new RestClient(constring);

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
        public AvailableTimesDTO GetReservationTimeByDate(DateTime date)
        {
            var constring = ConfigurationManager.ConnectionStrings["ServiceConString"].ConnectionString;
            var client = new RestClient(constring);
            var request = new RestRequest("/Table/timeSlot/{date}", Method.GET);
            request.AddUrlSegment("date", date.ToString("MM-dd-yy HH:mm:ss"));
            var content = client.Execute(request).Content;
            var res = JsonConvert.DeserializeObject<AvailableTimesDTO>(content);
            return res;
        }
        public List<TablesDTO> GetFreeTables(DateTime date)
        {
            List<TablesDTO> res = new List<TablesDTO>();
            string content = "Connention failure";
            try
            {
                var constring = ConfigurationManager.ConnectionStrings["ServiceConString"].ConnectionString;
                var client = new RestClient(constring);
                var request = new RestRequest("/Table/OpenTables/{date}", Method.GET);
                request.AddUrlSegment("date", date.ToString("MM-dd-yy HH:mm:ss")); 
                content = client.Execute(request).Content;
                res = JsonConvert.DeserializeObject<List<TablesDTO>>(content);
            }
            catch
            {
                MessageBox.Show(content);
            }

            return res;
        }
    }
}
