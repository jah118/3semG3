using Newtonsoft.Json;
using RestaurantClientService.DataTransferObjects;
using RestSharp;
using System.Collections.Generic;

namespace RestaurantClientService.Services.ReservationService
{
    public class ReservationRepository : IRepository<ReservationDTO>
    {
        private readonly string _constring;

        public ReservationRepository(string constring)
        {
            _constring = constring;
        }

        public ReservationDTO Create(ReservationDTO reservation)
        {
            ReservationDTO res = null;
            try
            {
                string constring = ConfigurationManager.ConnectionStrings["ServiceConString"].ConnectionString;
                var client = new RestClient(constring);
                string json = JsonConvert.SerializeObject(reservation);
                var request = new RestRequest("/reservation", Method.POST);
                request.AddJsonBody(json);
                var response = client.Execute(request).Content;
                res = JsonConvert.DeserializeObject<ReservationDTO>(response);
            }
            catch
            {
            }
            return res;
        }

        public ReservationDTO Get(int id)
        {
            ReservationDTO res = null;
            try
            {
                string constring = ConfigurationManager.ConnectionStrings["ServiceConString"].ConnectionString;
                var client = new RestClient(constring);
                var request = new RestRequest("/reservation/{Id}", Method.GET);
                request.AddUrlSegment("Id", id);
                var response = client.Execute(request).Content;
                res = JsonConvert.DeserializeObject<ReservationDTO>(response);
            }
            catch
            {
            }
            return res;
        }

        public IEnumerable<ReservationDTO> GetAll()
        {
            List<ReservationDTO> res = null;
            try
            {
                string constring = ConfigurationManager.ConnectionStrings["ServiceConString"].ConnectionString;
                var client = new RestClient(constring);
                var request = new RestRequest("/reservation", Method.GET);
                var content = client.Execute(request).Content;
                res = JsonConvert.DeserializeObject<List<ReservationDTO>>(content);
            }
            catch
            {
            }
            return res ?? new List<ReservationDTO>();
        }
    }
}