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
        private DataRowView _selectedItem;
        private readonly IReservationRepository repository = new ReservationRepository();
        public event PropertyChangedEventHandler PropertyChanged;

        public ManageReservationViewModel()
        {
        }
        private void CreateSearchTable()
        {
            _reservationTable = new DataTable();
            _reservationTable.Columns.Add(new DataColumn("Reservation Number"));
            _reservationTable.Columns.Add(new DataColumn("Customer"));
            _reservationTable.Columns.Add(new DataColumn("ReservationDate"));
            _reservationTable.Columns.Add(new DataColumn("ReservationTime"));
            _reservationTable.Columns.Add(new DataColumn("NoOfPeople"));
        }
        public DataRowView SelectedItem
        {
            get
            {
                return _selectedItem;
            }
            set
            {
                _selectedItem = value;
                var v = _selectedItem.Row.ItemArray[0];
                PropertyChanged(this, new PropertyChangedEventArgs("SelectedItem"));
            }
        }

        public DataTable SearchTable
        {
            get
            {
                List<ReservationDTO> _reservations = repository.GetAllReservations();
                if (_reservations != null)
                {
                    if (_reservationTable == null) CreateSearchTable();

                    _reservations.ForEach(x =>
                    {
                        DataRow dr = _reservationTable.NewRow();
                        _reservationTable.Rows.Add(dr.ItemArray = new[] { x.Id+"", x.Customer.FirstName + " " + x.Customer.LastName,
                            x.ReservationDate+"", x.ReservationTime+"", x.NoOfPeople +"" });
                    });
                }
                return _reservationTable;
            }
            set
            {
                _reservationTable = value;
                PropertyChanged(this, new PropertyChangedEventArgs("ReservationTable"));
            }
        }

    }
}
