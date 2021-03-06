﻿using System;
using System.Linq;
using DataAccess.DataTransferObjects;

namespace DataAccess.Models
{
    public partial class Reservation
    {
        public static bool Validate(ReservationDTO toValidate)
        {
            if (toValidate == null) return false;
            if (!Customer.Validate(toValidate.Customer)) return false;
            if (toValidate.ReservationTime <= DateTime.Now) return false;  
            if (0>=(toValidate.NoOfPeople)) return false;
            if (toValidate.Tables.ToList().Count <= 0) return false;
            return true;
        }
    }
}
