using RestaurantWebApp.DataTransferObject;
using RestaurantWebApp.Service.Interfaces;
using RestSharp;
using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;
using RestSharp.Extensions;
using Newtonsoft.Json;

namespace RestaurantWebApp.Service
{
    public class BookingServices : IBookingService
    {
        private readonly string _connectionString;

        public BookingServices(string constring)
        {
            _connectionString = constring;
        }

        public UserDTO GetUser(string v)
        {
            throw new System.NotImplementedException();
        }

        public IEnumerable<ReservationDTO> GetAll()
        {
            throw new System.NotImplementedException();
        }

        public ReservationDTO GetById(int id)
        {
            throw new System.NotImplementedException();
        }

        //public bool Create(ReservationDTO obj)
        //{
        //    var client = new RestClient(_connectionString);
        //    var request = new RestRequest("/Reservation", Method.POST);
        //    request.AddJsonBody(obj);
        //    var response = client.Execute(request).IsSuccessful;

        //    return response;
        //}

        public ReservationDTO Update(ReservationDTO obj)
        {
            throw new System.NotImplementedException();
        }

        public bool Delete(ReservationDTO obj)
        {
            throw new System.NotImplementedException();
        }

        public Task<IEnumerable<ReservationDTO>> GetAllAsync()
        {
            throw new System.NotImplementedException();
        }

        public Task<ReservationDTO> GetByIdAsync(int id)
        {
            throw new System.NotImplementedException();
        }

        public async Task<IRestResponse> CreateAsync(ReservationDTO obj)
        {
            var client = new RestClient(_connectionString);
            var request = new RestRequest("/Reservation", Method.POST);
            request.AddJsonBody(obj);
            var response = (await client.ExecuteAsync(request));
            return response;
        }

        public Task<ReservationDTO> UpdateAsync(ReservationDTO obj)
        {
            throw new System.NotImplementedException();
        }

        public Task<bool> DeleteAsync(ReservationDTO obj)
        {
            throw new System.NotImplementedException();
        }

        public ReservationDTO Create(ReservationDTO obj)
        {
            var client = new RestClient(_connectionString);
            var request = new RestRequest("/Reservation", Method.POST);
            request.AddJsonBody(obj);
            var content = client.Execute(request).Content;
            var res = JsonConvert.DeserializeObject<ReservationDTO>(content);

            return res;
        }
    }
}