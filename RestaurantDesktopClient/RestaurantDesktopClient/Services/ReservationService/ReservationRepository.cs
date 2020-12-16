using DataAccess.DataTransferObjects;
using Newtonsoft.Json;
using RestaurantDesktopClient.Views.ViewModels;
using RestSharp;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using RestaurantDesktopClient.Services;

namespace RestaurantDesktopClient.Reservation
{
    public class ReservationRepository : IRepository<ReservationDTO>
    {
        private readonly string _constring;
        private readonly IAuthRepository _authRepository;

        public ReservationRepository(string constring, IAuthRepository authRepository)
        {
            this._constring = constring;
            _authRepository = authRepository;
        }

        public ReservationDTO Create(ReservationDTO reservation)
        {
            try
            {
                var client = new RestClient(_constring);
                string json = JsonConvert.SerializeObject(reservation);
                var request = new RestRequest("/reservation", Method.POST);
                request.AddJsonBody(json);
                var response = client.Execute(request).Content;
                return JsonConvert.DeserializeObject<ReservationDTO>(response);
            }
            catch
            {
                return null;
            }
        }
        //TODO not used?
        public ReservationDTO Get(int id)
        {
            try
            {
                ReservationDTO res = null;
                var client = new RestClient(_constring);
                var request = new RestRequest("/reservation/{Id}", Method.GET);
                request.AddUrlSegment("Id", id);
                if (_authRepository.AddTokenToRequest(request))
                {
                    var response = client.Execute(request).Content;
                    res = JsonConvert.DeserializeObject<ReservationDTO>(response);
                }

                return res;
            }
            catch
            {
                return null;
            }
        }
        public ReservationDTO Update(ReservationDTO reservation)
        {
            try
            {
                ReservationDTO res = null;
                var client = new RestClient(_constring);
                string json = JsonConvert.SerializeObject(reservation);
                var request = new RestRequest("/reservation/{id}", Method.PUT);
                request.AddUrlSegment("id", reservation.Id);
                request.AddJsonBody(json);
                if (_authRepository.AddTokenToRequest(request))
                {

                    var response = client.Execute(request).Content;
                    res = JsonConvert.DeserializeObject<ReservationDTO>(response);
                }
                return res;
            }
            catch
            {
                return null;
            }
        }
        public HttpStatusCode Delete(ReservationDTO reservation)
        {
            try
            {
                var client = new RestClient(_constring);
                var request = new RestRequest("/reservation/{id}", Method.DELETE);
                request.AddUrlSegment("id", reservation.Id);
                _authRepository.AddTokenToRequest(request);
                return client.Execute(request).StatusCode;
            }
            catch
            {
                return HttpStatusCode.BadRequest;
            }
        }
        public IEnumerable<ReservationDTO> GetAll()
        {
            try
            {
                List<ReservationDTO> res = null;
                var client = new RestClient(_constring);
                var request = new RestRequest("/reservation", Method.GET);
                if (_authRepository.AddTokenToRequest(request))
                {
                    var content = client.Execute(request).Content;
                    res = JsonConvert.DeserializeObject<List<ReservationDTO>>(content);
                }

                return res;
            }
            catch
            {
                return new List<ReservationDTO>();
            }
        }
    }
}
