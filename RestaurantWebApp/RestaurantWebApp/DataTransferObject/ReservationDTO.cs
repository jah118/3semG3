using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace RestaurantWebApp.DataTransferObject
{
    public class ReservationDTO
    {
        public ReservationDTO()
        {
            ReservationDate = DateTime.Now;
            Deposit = false;
            OrderingFood = false;
        }

        public ReservationDTO(int id)
        {
            Id = id;
        }

        //for testing
        public ReservationDTO(int id, CustomerDTO customer, DateTime reservationTime, int noOfPeople,
            bool deposit, string note, IEnumerable<RestaurantTablesDTO> restaurantTables) : this()
        {
            Id = id;
            Customer = customer;
            ReservationTime = reservationTime;
            NoOfPeople = noOfPeople;
            Deposit = deposit;
            Note = note;
            Tables = restaurantTables;
        }

        public int Id { get; set; }
        public DateTime ReservationDate { get; }

        [Required] public CustomerDTO Customer { get; set; }

        [Required]
        [Display(Name = "Reservationstidspunkt   ")]
        [DisplayFormat(DataFormatString = "{dd-MM-yyyy HH:mm:ss}")]
        public DateTime ReservationTime { get; set; }

        [Required]
        [Range(1, 25)]
        [Display(Name = "Antal Personer")]
        public int NoOfPeople { get; set; }

        public bool? Deposit { get; }

        [Display(Name = "Evt Notat")] public string Note { get; set; }

        [Required] [Display(Name = "Borde ")] public IEnumerable<RestaurantTablesDTO> Tables { get; set; }

        //public IEnumerable<ReservationTimesDTO> TimeSlots { get; set; }
        //public IEnumerable<AvailableTimesDTO> TimeSlots { get; set; }
        public AvailableTimesDTO TimeSlots { get; set; }
        public bool OrderingFood { get; set; }
    }
}