using DataAccess.DataTransferObjects;
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

        List<ReservationDTO> IReservationRepository.GetAllReservations()
        {
            List<ReservationDTO> res = new List<ReservationDTO> { new ReservationDTO {ReservationDate = DateTime.Now,
                Customer = new CustomerDTO { Phone = "1231231", Email = "mailen der er her", FirstName = "Jonna",
                    LastName = "Jonnasen", Address="Jonnavej 32", City = "JonnaBy", ZipCode = "3020" }, 
                    ReservationTime = DateTime.Now, NoOfPeople = 4, Note = "Some awesome note!!" } };
            //TODO: import From Rest service...


            return res;
        }
    }
}
