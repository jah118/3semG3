using DataAccess.DataTransferObjects;
using RestaurantDesktopClient.Reservation;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Navigation;

namespace RestaurantDesktopClient.Views.ManageReservation
{
    class ManageReservationViewModel : INotifyPropertyChanged
    {
        private DataTable _reservationTable;
        private IReservationRepository repository = new ReservationRepository();

        public ManageReservationViewModel()
        {
        }

        public DataTable SearchTable
        {
            get {
                updateSearchTable();
                return _reservationTable; }
            set
            {
                _reservationTable = value;
                PropertyChanged(this, new PropertyChangedEventArgs("ReservationTable"));
            }                
        }

        private DataTable ConvertReservationObjToDataTable(List<ReservationDTO> input)
        {
            DataTable res = new DataTable();
            foreach (PropertyInfo info in typeof(ReservationDTO).GetProperties())
            {
                res.Columns.Add(new DataColumn(info.Name));
            }
            input.ForEach((x) => {
                DataRow dr = res.NewRow();
                dr["Customer"] = x.Customer.FirstName + " " + x.Customer.LastName;
                dr["ReservationDate"] = x.ReservationDate.ToString();
                dr["ReservationTime"] = x.ReservationTime.ToString();
                dr["NoOfPeople"] = x.NoOfPeople;
                res.Rows.Add(dr);
            });
            return res;
        }

        public event PropertyChangedEventHandler PropertyChanged;

        public void updateSearchTable()
        {
            SearchTable = ConvertReservationObjToDataTable(repository.GetAllReservations());
        }

    }
}
