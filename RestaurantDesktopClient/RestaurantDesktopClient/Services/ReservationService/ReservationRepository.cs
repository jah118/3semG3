using DataAccess.DataTransferObjects;
using Newtonsoft.Json;
using RestaurantDesktopClient.Views.ViewModels;
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
    public class ReservationRepository : IRepository<ReservationDTO>
    {
        private readonly string _constring;

        public ReservationRepository()
        {

        }

        public ReservationRepository(string constring)
        {
            this._constring = constring;
        }

        public ReservationDTO Create(ReservationDTO reservation)
        {
            ReservationDTO res = null;
            try
            {
                var client = new RestClient(_constring);
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
                var client = new RestClient(_constring);
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
                var client = new RestClient(_constring);
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
