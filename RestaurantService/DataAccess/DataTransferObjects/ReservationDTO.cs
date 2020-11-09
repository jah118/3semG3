﻿using System;
using System.Collections.Generic;

namespace DataAccess.DataTransferObjects
{
    public class ReservationDTO
    {
        public ReservationDTO()
        {
        }

        public ReservationDTO(int id)
        {
            Id = id;
        }

        public int Id { get; }
        public DateTime ReservationDate { get; set; }
        public CustomerDTO Customer { get; set; }
        public DateTime ReservationTime { get; set; }
        public int NoOfPeople { get; set; }
        public bool? Deposit { get; set; }
        public string Note { get; set; }
        public IEnumerable<RestaurantTablesDTO> Tables { get; set; }
    }
}