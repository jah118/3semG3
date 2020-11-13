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
                UpdateSearchTable();
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
            res.Columns.Add(new DataColumn("Reservation Number"));
            res.Columns.Add(new DataColumn("Customer"));
            res.Columns.Add(new DataColumn("ReservationDate"));
            res.Columns.Add(new DataColumn("ReservationTime"));
            res.Columns.Add(new DataColumn("NoOfPeople"));
            input.ForEach((x) => {
                DataRow dr = res.NewRow();
                dr["Reservation Number"] = x.Id;
                dr["Customer"] = x.Customer.FirstName + " " + x.Customer.LastName;
                dr["ReservationDate"] = x.ReservationDate.ToString();
                dr["ReservationTime"] = x.ReservationTime.ToString();
                dr["NoOfPeople"] = x.NoOfPeople;
                res.Rows.Add(dr);
            });
            return res;
        }

        public event PropertyChangedEventHandler PropertyChanged;

        public void UpdateSearchTable()
        {
            var reservations = repository.GetAllReservations();
            if(reservations!= null)
            {
            SearchTable = ConvertReservationObjToDataTable(reservations);
            }
        }

    }
}
