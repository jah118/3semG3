using Newtonsoft.Json;
using RestaurantDesktopClient.DataTransferObject;
using System;
using System.Collections.Generic;

namespace DataAccess.DataTransferObjects
{
    public class OrderDTO
    {
        public int OrderNo { get; set; }
        public int ReservationID { get; set; }
        public int EmployeeID { get; set; }
        public DateTime OrderDate { get; set; }
        public string PaymentCondition { get; set; }
        [JsonProperty("orderLines")]
        public List<OrderLineDTO> OrderLines { get; set; }

    }
}
