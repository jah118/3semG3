using DataAccess.DataTransferObjects;
using Newtonsoft.Json;
using RestSharp;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RestaurantDesktopClient.Reservation
{
    class ReservationRepository : IReservationRepository
    {
        public void CreateReservation(ReservationDTO reservation)
        {
            try
            {
                var client = new RestClient("https://localhost:44349/api");
                string json = JsonConvert.SerializeObject(reservation);
                var request = new RestRequest("/reservation", Method.POST);
                request.AddJsonBody(json);
                var response = client.Execute(request).Content;
            }
            catch
            {
            }
        }

        public ReservationDTO GetReservation(int id)
        {
            ReservationDTO res = null;
            try
            {
                var client = new RestClient("https://localhost:44349/api");
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

        List<ReservationDTO> IReservationRepository.GetAllReservations()
        {

            var client = new RestClient("https://localhost:44349/api");
            var request = new RestRequest("/reservation", Method.GET);
            var content = client.Execute(request).Content;
            var res = JsonConvert.DeserializeObject<List<ReservationDTO>>(content);


            return (List<ReservationDTO>)res;
        }

    }
}
