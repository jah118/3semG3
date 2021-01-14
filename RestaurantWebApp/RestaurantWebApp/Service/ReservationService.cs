using System;
using System.Collections.Generic;
using System.Net;
using Newtonsoft.Json;
using RestaurantWebApp.DataTransferObject;
using RestaurantWebApp.Service.Interfaces;
using RestSharp;

namespace RestaurantWebApp.Service
{
    public class ReservationService : IReservationService
    {
        private readonly IAuthService _authService;
        private readonly string _connectionString;

        public ReservationService(string connectionString, IAuthService authRepository)
        {
            _connectionString = connectionString;
            _authService = authRepository;
        }

        public IEnumerable<ReservationDTO> GetAll()
        {
            throw new NotImplementedException();
        }

        public ReservationDTO GetById(int id)
        {
            var client = new RestClient(_connectionString);
            var request = new RestRequest("/Reservation/{id}", Method.GET);
            request.AddParameter("Id", id);
            //_authRepository.AddTokenToRequest(request);
            var content = client.Execute(request).Content;
            var res = JsonConvert.DeserializeObject<ReservationDTO>(content);
            return res;
        }

        public ReservationDTO Update(ReservationDTO obj)
        {
            throw new NotImplementedException();
        }

        public bool Delete(ReservationDTO obj)
        {
            throw new NotImplementedException();
        }

        public AvailableTimesDTO GetReservationTimeByDate(DateTime date)
        {
            var client = new RestClient(_connectionString);
            var request = new RestRequest("/Reservation/timeSlot/{date}", Method.GET);
            request.AddUrlSegment("date", date);
            var content = client.Execute(request).Content;
            var res = JsonConvert.DeserializeObject<AvailableTimesDTO>(content);
            return res;
        }

        public ReservationDTO Create(ReservationDTO obj)
        {
            var client = new RestClient(_connectionString);
            var request = new RestRequest("/Reservation", Method.POST);
            var json = JsonConvert.SerializeObject(obj);
            request.AddJsonBody(json);
            var content = client.Execute(request).Content;
            var res = JsonConvert.DeserializeObject<ReservationDTO>(content);

            return res;
        }

        public IEnumerable<ReservationDTO> GetReservationByCustomerId()
        {
            IEnumerable<ReservationDTO> res = new List<ReservationDTO>();
            var client = new RestClient(_connectionString);
            var request = new RestRequest("/Reservation/CustomerReservations", Method.GET);
            if (_authService.AddTokenToRequest(request))
            {
                var content = client.Execute(request);

                if (content.StatusCode == HttpStatusCode.OK)
                    res = JsonConvert.DeserializeObject<IEnumerable<ReservationDTO>>(content.Content);
            }

            return res;
        }
    }
}