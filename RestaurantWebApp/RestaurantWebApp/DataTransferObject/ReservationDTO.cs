﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace RestaurantWebApp.DataTransferObject
{
    public class ReservationDTO
    {
        public ReservationDTO()
        {
            _reservationDate = DateTime.Now;
        }

        //for testing
        public ReservationDTO(CustomerDTO customer, DateTime reservationTime, int noOfPeople,
            bool deposit, string note, IEnumerable<RestaurantTablesDTO> restaurantTables) :this()
        {
            Customer = customer;
            ReservationTime = reservationTime;
            NoOfPeople = noOfPeople;
            Deposit = deposit;
            Note = note;
            Tables = restaurantTables;
        }

        public int Id { get; }
        private DateTime _reservationDate;

        [Required] public CustomerDTO Customer { get; set; }

        [Required]
        [Display(Name = "Reservationstidspunkt")]
        [DisplayFormat(DataFormatString = "{dd-MM-yyyy HH:mm:ss}", ApplyFormatInEditMode = true)]
        public DateTime? ReservationTime { get; set; }

        [Required]
        [Range(1, 25)]
        [Display(Name = "Antal Personer")]
        public int NoOfPeople { get; set; }

        public bool? Deposit { get; }

        [Display(Name = "Evt Notat")] public string Note { get; set; }

        [Required] [Display(Name = "Borde ")] public IEnumerable<RestaurantTablesDTO> Tables { get; set; }

        public IEnumerable<ReservationTimesDTO> TimeSlots { get; set; }
    }
}