using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.DataTransferObjects
{
    public class ReservationDTO
    {
        public ReservationDTO()
        {
            ReservationDate = DateTime.Now;
        }

        public int Id { get; }
        public DateTime ReservationDate { get; set; }
        public CustomerDTO Customer { get; set; }
        [DisplayFormat(DataFormatString = "{dd-MM-yyyy HH:mm:ss}", ApplyFormatInEditMode = true)]
        public DateTime ReservationTime { get; set; }
        public int NoOfPeople { get; set; }
        public bool? Deposit { get; }
        public string Note { get; set; }

        public IEnumerable<RestaurantTablesDTO> Tables { get; set; }


        //for testing
        public ReservationDTO(DateTime reservationDate, CustomerDTO customer, DateTime reservationTime, int noOfPeople, bool deposit, string note, IEnumerable<RestaurantTablesDTO> restaurantTables)
        {
            ReservationDate = reservationDate;
            Customer = customer;
            ReservationTime = reservationTime; 
            NoOfPeople = noOfPeople;
            Deposit = deposit;
            Note = note;
            Tables = restaurantTables;
        }
    }
}
