using DataAccess.DataTransferObjects;
using Newtonsoft.Json;
using RestSharp;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace RestaurantWebApp.Service
{
    public class BokingServices
    {
        public IEnumerable<RestaurantTablesDTO> GetBookingTables(string constring)
        {
            var client = new RestClient(constring);
            var request = new RestRequest("/Table", Method.GET);
            var content =  client.Execute(request).Content;

            var res = JsonConvert.DeserializeObject<IEnumerable<RestaurantTablesDTO>>(content, new JsonSerializerSettings
            {
                ConstructorHandling = ConstructorHandling.AllowNonPublicDefaultConstructor
            });
            return res;
        }


        public async Task<IRestResponse> PostBookingAsync(ReservationDTO reservation, string constr)
        {
            var client = new RestClient(constr);
            //var client = new RestClient("https://localhost:44349/api/Booking/Create");
            //string json = JsonConvert.SerializeObject(reservation);
            var request = new RestRequest("/post", Method.POST);
           //  var request = new RestRequest("/Reservation", Method.POST);
            request.AddJsonBody(reservation);
            var response = await client.ExecuteAsync(request);

            return response;
        }

    }
}