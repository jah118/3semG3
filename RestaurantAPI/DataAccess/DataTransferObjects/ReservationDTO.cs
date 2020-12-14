using System;
using System.Collections.Generic;

namespace DataAccess.DataTransferObjects
{
    public class ReservationDTO
    {
        public ReservationDTO()
        {
            
        }
        public ReservationDTO(int id, DateTime reservationDate, CustomerDTO customer, DateTime reservationTime, int noOfPeople, bool? deposit, string note, IEnumerable<RestaurantTablesDTO> tables)
        {
            Id = id;
            ReservationDate = reservationDate;
            Customer = customer;
            ReservationTime = reservationTime;
            NoOfPeople = noOfPeople;
            Deposit = deposit;
            Note = note;
            Tables = tables;
        }
        public int Id { get; init; }
        public DateTime ReservationDate { get; set; }
        public CustomerDTO Customer { get; set; }
        public DateTime ReservationTime { get; set; }
        public int NoOfPeople { get; set; }
        public bool? Deposit { get; set; }
        public string Note { get; set; }
        public IEnumerable<RestaurantTablesDTO> Tables { get; set; }
    }
}