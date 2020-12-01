using DataTransferObjects;
using Newtonsoft.Json;
using RestSharp;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace RestaurantWebAppCore.Service
{
    public class BookingServices
    {
        private readonly string _connectionString;

        public BookingServices(string ConnectionString)
        {
            _connectionString = ConnectionString;
        }

        public IEnumerable<RestaurantTablesDTO> GetBookingTables()
        {
            var client = new RestClient(_connectionString);
            var request = new RestRequest("/Table", Method.GET);
            var content =  client.Execute(request).Content;

            var res = JsonConvert.DeserializeObject<IEnumerable<RestaurantTablesDTO>>(content); 
            //var res = JsonConvert.DeserializeObject<IEnumerable<RestaurantTablesDTO>>(content, new JsonSerializerSettings
            //{
            //    ConstructorHandling = ConstructorHandling.AllowNonPublicDefaultConstructor
            //});
            return res;
        }
        public IEnumerable<FoodDTO> GetAllFoods(string constring)
        {
            var client = new RestClient(constring);
            var request = new RestRequest("/Food", Method.GET);
            var content = client.Execute(request).Content;

            var res = JsonConvert.DeserializeObject<IEnumerable<FoodDTO>>(content);


            return res;
        }


        public async Task<IRestResponse> PostBookingAsync(ReservationDTO reservation)
        {
            var client = new RestClient(_connectionString);
            //var client = new RestClient("https://localhost:44349/api/Booking/Create");
            //string json = JsonConvert.SerializeObject(reservation);
           // var request = new RestRequest("/post", Method.POST);
             var request = new RestRequest("/Reservation", Method.POST);
            request.AddJsonBody(reservation);
            var response = await client.ExecuteAsync(request);

            return response;
        }

        public UserDTO GetUser(string v)
        {
            throw new System.NotImplementedException();
        }
    }
}