using Newtonsoft.Json;
using System;
using System.Collections.Generic;

namespace RestaurantClientService.DataTransferObjects
{
    public class OrderDTO
    {
        public int OrderNo { get; set; }
        public int ReservationID { get; set; }
        public int EmployeeID { get; set; }
        public DateTime OrderDate { get; set; }
        public string PaymentCondition { get; set; }
        [JsonProperty("orderLines")]
        internal List<OrderLineDTO> OrderLines { get; set; }


    }
}
