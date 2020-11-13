using DataAccess.DataTransferObjects;
using Newtonsoft.Json;
using RestSharp;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RestaurantDesktopClient.Reservation
{
    public class ReservationRepository : IReservationRepository
    {
        public ReservationRepository()
        {

        }
        public System.Net.HttpStatusCode CreateReservation(ReservationDTO reservation)
        {
            System.Net.HttpStatusCode res = System.Net.HttpStatusCode.Unused;
            try
            {
                string constring = ConfigurationManager.ConnectionStrings["ServiceConString"].ConnectionString;
                var client = new RestClient(constring);
                string json = JsonConvert.SerializeObject(reservation);
                var request = new RestRequest("/reservation", Method.POST);
                request.AddJsonBody(json);
                res = client.Execute(request).StatusCode;
                
            }
            catch
            {
            }
            return res;
        }

        public ReservationDTO GetReservation(int id)
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

        List<ReservationDTO> IReservationRepository.GetAllReservations()
        {

            string constring = ConfigurationManager.ConnectionStrings["ServiceConString"].ConnectionString;
            var client = new RestClient(constring);
            var request = new RestRequest("/reservation", Method.GET);
            var content = client.Execute(request).Content;
            var res = JsonConvert.DeserializeObject<List<ReservationDTO>>(content);


            return (List<ReservationDTO>)res;
        }

    }
}
