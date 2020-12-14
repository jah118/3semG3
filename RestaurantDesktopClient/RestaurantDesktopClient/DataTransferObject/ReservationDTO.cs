using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace DataAccess.DataTransferObjects
{
    public class ReservationDTO : INotifyPropertyChanged
    {
        public int Id { get; set; }
        public DateTime ReservationDate { get; set; }
        public CustomerDTO Customer { get; set; }
        public DateTime ReservationTime { get; set; }
        public int NoOfPeople { get; set; }
        public bool Deposit { get; set; }
        public string Note { get; set; }
        private List<TablesDTO> _tables = new List<TablesDTO>();
        public List<TablesDTO> Tables { get{ return _tables; }
            set { _tables = value; OnPropertyChanged("_tables"); }
        }

        public ReservationDTO()
        {
            ReservationDate = DateTime.Now;
            ReservationTime = DateTime.Now;
        }

        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged([CallerMemberName] string name = null)
        {
            this.PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
        }
    }
}