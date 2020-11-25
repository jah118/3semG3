using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataTransferObjects
{
    public class ReservationDTO
    {
        public ReservationDTO()
        {
            ReservationDate = DateTime.Now;
        }

        public int Id { get; }
        public DateTime ReservationDate { get; set; }
        [Required]
        public CustomerDTO Customer { get; set; }
        [Required,Display(Name = "Reservationstidspunkt"), DisplayFormat(DataFormatString = "{dd-MM-yyyy HH:mm:ss}", ApplyFormatInEditMode = true)]
        public DateTime ReservationTime { get; set; }
        [Required,Range(1,25), Display(Name = "Antal Personer")]
        public int NoOfPeople { get; set; }
        public bool? Deposit { get; }
        [Display(Name = "Evt Notat")]
        public string Note { get; set; }
        [Required,Display(Name ="Borde ")]
        public IEnumerable<RestaurantTablesDTO> Tables { get; set; }

        public IEnumerable<ReservationTimesDTO> TimeSlots { get; set; }


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
