using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.DataTransferObjects
{
    public class ReservationDTO
    {
        public ReservationDTO()
        {
           

            ReservationDate = DateTime.Now;
            ReservationTime = DateTime.Now;

        }
        public int Id { get; }
        public DateTime ReservationDate { get; set; }
        public CustomerDTO Customer { get; set; } //TODO tjeck om dette er virtuel eller ej
        public DateTime ReservationTime { get; set; }
        public int NoOfPeople { get; set; }
        public bool Deposit { get; }
        public string Note { get; set; }

        public ICollection<ReservationsTablesDTO> ReservationsTables { get;}


    }
}
