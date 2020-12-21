using System.Collections.Generic;
using System.Net;
using Newtonsoft.Json;
using RestaurantDesktopClient.DataTransferObject;
using RestSharp;

namespace RestaurantDesktopClient.Services.ReservationService
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
                var client = new RestClient(_constring);
                string json = JsonConvert.SerializeObject(reservation);
                var request = new RestRequest("/reservation", Method.POST);
                request.AddJsonBody(json);
                var response = client.Execute(request);

                return response.StatusCode == HttpStatusCode.OK ? JsonConvert.DeserializeObject<ReservationDTO>(response.Content) : null;
        }

        public ReservationDTO Get(int id)
        {
                ReservationDTO res = null;
                var client = new RestClient(_constring);
                var request = new RestRequest("/reservation/{Id}", Method.GET);
                request.AddUrlSegment("Id", id);

                if (_authRepository.AddTokenToRequest(request))
                {
                    var response = client.Execute(request);
                    res = response.StatusCode == HttpStatusCode.OK ? JsonConvert.DeserializeObject<ReservationDTO>(response.Content) : null;
                }

                return res;

        }

        public ReservationDTO Update(ReservationDTO reservation)
        {
                ReservationDTO res = null;
                var client = new RestClient(_constring);
                string json = JsonConvert.SerializeObject(reservation);
                var request = new RestRequest("/reservation/{id}", Method.PUT);
                request.AddUrlSegment("id", reservation.Id);
                request.AddJsonBody(json);

                if (_authRepository.AddTokenToRequest(request))
                {

                    var response = client.Execute(request);
                    res = response.StatusCode == HttpStatusCode.OK ? JsonConvert.DeserializeObject<ReservationDTO>(response.Content) : null;
                }

                return res;

        }

        public HttpStatusCode Delete(ReservationDTO reservation)
        {
                var client = new RestClient(_constring);
                var request = new RestRequest("/reservation/{id}", Method.DELETE);
                request.AddUrlSegment("id", reservation.Id);
                _authRepository.AddTokenToRequest(request);

                return client.Execute(request).StatusCode;
        }

        public IEnumerable<ReservationDTO> GetAll()
        {
                List<ReservationDTO> res = null;
                var client = new RestClient(_constring);
                var request = new RestRequest("/reservation", Method.GET);

                if (_authRepository.AddTokenToRequest(request))
                {
                    var response = client.Execute(request);
                    res = response.StatusCode == HttpStatusCode.OK ? 
                        JsonConvert.DeserializeObject<List<ReservationDTO>>(response.Content) : new List<ReservationDTO>();
                }

                return res;
        }
    }
}
