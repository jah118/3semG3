using DataAccess.DataTransferObjects;
using RestaurantDesktopClient.Controllers;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace RestaurantDesktopClient.Views.Controls
{
    /// <summary>
    /// Interaction logic for ReservationViewControl.xaml
    /// </summary>
    public partial class ReservationViewControl : UserControl
    {
        private ReservationDTO _reservation;
        public ReservationViewControl()
        {
            InitializeComponent();
            InsertReservationTables();
        }

        private void InsertReservationTables()
        {
            lvTableNames.ItemsSource = new TableController().getAllTables();
            lvTableNames.DisplayMemberPath = "TableNumber";
        }

        public void SetReservationInformation(ReservationDTO reservation)
        {
            _reservation = reservation;
            cbDepositPayed.IsChecked = reservation.Deposit;
            txtCustomerNumber.Text = reservation.Customer.Id + "";
            lvTableNames.ItemsSource = reservation.RestaurantTable;
            dtpReservationTime.setDateTime(reservation.ReservationTime);
            dpReservationDate.SelectedDate = reservation.ReservationDate;
            txtNumOfPersons.Text = reservation.NoOfPeople + "";
            txtReservationComments.Text = reservation.Note;
            reservation.Note = txtReservationComments.Text;
            MarkBookedTables(_reservation.RestaurantTable);
        }

        private void MarkBookedTables(List<RestaurantTablesDTO> restaurantTables)
        {
            if(restaurantTables != null)
            {
                restaurantTables.ForEach((x) =>
                {
                    DataRowView drv = (DataRowView)lvTableNames.Items[lvTableNames.Items.IndexOf(x)];
                    lvTableNames.SelectedItems.Add(drv);

                });
            }

        }
    }
}
