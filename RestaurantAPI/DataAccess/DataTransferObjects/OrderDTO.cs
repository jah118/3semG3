using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.DataTransferObjects
{
    public class OrderDTO
    {
        public int OrderNo { get; set; }
        public int ReservationID { get; set; }
        public EmployeeDTO Employee { get; set; }
        public DateTime OrderDate { get; set; }
        public string PaymentCondition { get; set; }
        public List<FoodDTO> Foods { get; set; }

    }
}
