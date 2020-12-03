using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.DataTransferObjects
{
    public class ReservationDTO
    {
        public int Id { get; set; }
        public DateTime ReservationDate { get; set; }
        public CustomerDTO Customer { get; set; }
        public DateTime ReservationTime { get; set; }
        public int NoOfPeople { get; set; }
        public bool Deposit { get; set; }
        public string Note { get; set; }
        public List<TablesDTO> Tables { get; set; }

        public ReservationDTO()
        {
            this.ReservationDate = DateTime.Now;
            this.ReservationTime = DateTime.Now;
        }
    }
}