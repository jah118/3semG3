using RestaurantWebApp.DataTransferObject;
using RestaurantWebApp.Service.Interfaces;
using RestSharp;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace RestaurantWebApp.Service
{
    public class BookingServices : IBookingService
    {
        private readonly string _constring;

        public BookingServices(string constring)
        {
            _constring = constring;
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

        public bool Create(ReservationDTO obj)
        {
            var client = new RestClient(_constring);
            var request = new RestRequest("/Reservation", Method.POST);
            request.AddJsonBody(obj);
            var response = client.Execute(request).IsSuccessful;

            return response;
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
            var response = (await client.ExecuteAsync(request)).IsSuccessful;

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