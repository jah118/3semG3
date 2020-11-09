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
                var client = new RestClient("https://ptsv2.com/t/ptsTempApiHost");
                //var client = new RestClient("https://localhost:44349/api/");
                string json = JsonConvert.SerializeObject(reservation);
                var request = new RestRequest("/post", Method.POST);
                request.RequestFormat = DataFormat.Json;
                request.AddJsonBody(reservation);
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
                var client = new RestClient("https://localhost:44349/api/");
                var request = new RestRequest("Booking/Get/Id", Method.POST);
                request.RequestFormat = DataFormat.Json;
                request.AddUrlSegment("Id", id);
                var response = client.Execute(request).Content;
                res = (ReservationDTO)JsonConvert.DeserializeObject(response);
            }
            catch
            {
            }
            res = new ReservationDTO { Id = 0, ReservationDate = DateTime.Now, Customer = new CustomerDTO {    //TODO: Remove when service is running...
                Phone = "1231231", Email = "mailen der er her", FirstName = "Jonna", LastName = "Jonnasen", Address = "Jonnavej 32",
                City = "JonnaBy", ZipCode = "3020" },
                ReservationTime = DateTime.Now.AddDays(1), NoOfPeople = 4, Note = "Some awesome note!!", Deposit = true };
            return res;
        }

        List<ReservationDTO> IReservationRepository.GetAllReservations()
        {
            var client = new RestClient("https://localhost:44349/api/reservation");

            var request = new RestRequest("Reservation", Method.GET);

            var content = client.Execute(request).Content;

            //TODO: convert from json to Reservation

            List<ReservationDTO> res = (List<ReservationDTO>) JsonConvert.DeserializeObject(content);

            res = new List<ReservationDTO> { new ReservationDTO { Id = 0, ReservationDate = DateTime.Now,
                Customer = new CustomerDTO { Phone = "1231231", Email = "mailen der er her", FirstName = "Jonna",
                    LastName = "Jonnasen", Address="Jonnavej 32", City = "JonnaBy", ZipCode = "3020" },
                    ReservationTime = DateTime.Now, NoOfPeople = 4, Note = "Some awesome note!!" }, new ReservationDTO { Id = 1, ReservationDate = DateTime.Now,
                Customer = new CustomerDTO { Phone = "12334531", Email = "mailen også der er her", FirstName = "Bente",
                    LastName = "Bentesen", Address="Bentevej 32", City = "BenteBy", ZipCode = "3020" },
                    ReservationTime = DateTime.Now, NoOfPeople = 4, Note = "Some awesome note!!" }  };

            return res;
        }

    }
}
