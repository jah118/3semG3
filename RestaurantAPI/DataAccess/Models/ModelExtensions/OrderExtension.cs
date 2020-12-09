using System;
using DataAccess.DataTransferObjects;

namespace DataAccess.Models
{
    public partial class RestaurantOrder
    {
        public static bool Validate(OrderDTO toValidate)
        {
            if (toValidate == null) return false;
            if (0>=toValidate.ReservationID) return false;
            if (0>=toValidate.EmployeeID) return false;
            if (string.IsNullOrEmpty(toValidate.PaymentCondition)) return false;
            if (toValidate.OrderLines.Count <= 0) return false;
          //TODO VALIDATION
            return true;
        }
    }
}
