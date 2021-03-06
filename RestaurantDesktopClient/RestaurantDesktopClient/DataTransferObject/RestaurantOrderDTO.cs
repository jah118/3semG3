﻿using System;

namespace RestaurantDesktopClient.DataTransferObject
{
    public class RestaurantOrderDTO
    {

        public int OrderNo { get; }

        public  ReservationDTO reservation { get; set; }

        public EmployeeDTO employee { get; set; }

        public DateTime OrderDate { get; set; }

        public PaymentCondition paymentCondition { get; set; }

    }
}