using DataAccess.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace DataAccess.DataTransferObjects.Converters
{
    public partial class Converter
    {
        //Unsure if this makes more sense than disretely handling dto -> Model mapping, and having a secondary constructor
        public static Reservation Convert(ReservationDTO obj)
        {
            return new Reservation()
            {
                CustomerId = obj.Customer.Id,
                Deposit = obj.Deposit,
                NoOfPeople = obj.NoOfPeople,
                Note = obj.Note,
                ReservationDate = obj.ReservationDate,
                ReservationTime = obj.ReservationTime
            };
        }

        public static ReservationDTO Convert(Reservation obj)
        {
            return new ReservationDTO(obj.Id)
            {
                Customer = new CustomerDTO(obj.Customer),
                Deposit = obj.Deposit,
                NoOfPeople = obj.NoOfPeople,
                Tables = Convert(obj.ReservationsTables.Where(rt => rt.ReservationId.Equals(obj.Id)).Select(t => t.RestaurantTables)),
                Note = obj.Note,
                ReservationDate = obj.ReservationDate,
                ReservationTime = obj.ReservationTime
            };
        }
    }
}