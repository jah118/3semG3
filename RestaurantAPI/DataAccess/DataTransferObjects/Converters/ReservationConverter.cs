﻿using DataAccess.Models;
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
            return new ReservationDTO()
            {
                Id = obj.Id,
                Customer = Convert(obj.Customer),
                Deposit = obj.Deposit,
                NoOfPeople = obj.NoOfPeople,
                Tables = Convert(obj.ReservationsTables.Where(rt => rt.ReservationId.Equals(obj.Id)).Select(t => t.RestaurantTables)),
                Note = obj.Note,
                ReservationDate = obj.ReservationDate,
                ReservationTime = obj.ReservationTime
            };
        }

        public static IEnumerable<ReservationDTO> Convert(IEnumerable<Reservation> obj)
        {
            var list = new List<ReservationDTO>();
            foreach (var t in obj)
            {
                list.Add(Convert(t));
            }
            return list;
        }

        public static IEnumerable<Reservation> Convert(IEnumerable<ReservationDTO> obj)
        {
            var list = new List<Reservation>();
            foreach (var t in obj)
            {
                list.Add(Convert(t));
            }
            return list;
        }

    }
}