using Newtonsoft.Json;
using RestSharp;
using System.Collections.Generic;
using System.Threading.Tasks;
using RestaurantWebApp.DataTransferObject;
using RestaurantWebApp.Service.Interfaces;

namespace RestaurantWebApp.Service
{
    public class BookingServices : IBookingService
    {
        private readonly string _constring;

        public BookingServices(string constring)
        {
            _constring = constring;
        }

        public async Task<IRestResponse> PostBookingAsync(ReservationDTO reservation)
        {
            var client = new RestClient(_constring);
            var request = new RestRequest("/Reservation", Method.POST);
            request.AddJsonBody(reservation);
            var response = await client.ExecuteAsync(request);

            return response;
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

        public ReservationDTO Create(ReservationDTO obj)
        {
            throw new System.NotImplementedException();
        }

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

        public async Task<bool> CreateAsync(ReservationDTO obj)
        {
            var client = new RestClient(_constring);
            var request = new RestRequest("/Reservation", Method.POST);
            request.AddJsonBody(obj);
            var response =  (await client.ExecuteAsync(request)).IsSuccessful;

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
    }
}