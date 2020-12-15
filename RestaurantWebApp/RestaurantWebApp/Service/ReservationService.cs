using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Newtonsoft.Json;
using RestaurantWebApp.DataTransferObject;
using RestaurantWebApp.Service.Interfaces;
using RestSharp;

namespace RestaurantWebApp.Service
{
    public class ReservationService : IReservationService
    {
        private readonly string _connectionString;

        public ReservationService(string constring)
        {
            _connectionString = constring;
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

        public Task<IEnumerable<ReservationDTO>> GetAllAsync()
        {
            throw new NotImplementedException();
        }

        public async Task<ReservationDTO> GetByIdAsync(int id)
        {
            var client = new RestClient(_connectionString);
            var request = new RestRequest("/Reservation/{id}", Method.GET);

            request.AddParameter("Id", id);
            var content = (await client.ExecuteAsync(request)).Content;
            var res = JsonConvert.DeserializeObject<ReservationDTO>(content);
            return res;
        }

        public async Task<IRestResponse> CreateAsync(ReservationDTO obj)
        {
            var client = new RestClient(_connectionString);
            var request = new RestRequest("/Reservation", Method.POST);
            request.AddJsonBody(obj);
            var response = await client.ExecuteAsync(request);
            return response;
        }

        public Task<ReservationDTO> UpdateAsync(ReservationDTO obj)
        {
            throw new NotImplementedException();
        }

        public Task<bool> DeleteAsync(ReservationDTO obj)
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
            string json = JsonConvert.SerializeObject(obj);
            request.AddJsonBody(json);
            var content = client.Execute(request).Content;
            var res = JsonConvert.DeserializeObject<ReservationDTO>(content);

            return res;
        }
    }
}