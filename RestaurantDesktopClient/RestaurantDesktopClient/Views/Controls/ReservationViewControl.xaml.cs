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
            cbDepositPayed.IsChecked = _reservation.Deposit;
            txtCustomerNumber.Text = _reservation.Customer.Id + "";
            lvTableNames.ItemsSource = _reservation.Tables;
            dtpReservationTime.setDateTime(_reservation.ReservationTime);
            dpReservationDate.SelectedDate = _reservation.ReservationDate;
            txtNumOfPersons.Text = _reservation.NoOfPeople + "";
            txtReservationComments.Text = _reservation.Note;
            MarkBookedTables(_reservation.Tables);
        }

        private void MarkBookedTables(List<TablesDTO> Tables)
        {
            if(Tables != null)
            {
                Tables.ForEach((x) =>
                {
                    var item = lvTableNames.Items[lvTableNames.Items.IndexOf(x)];
                    lvTableNames.SelectedItems.Add(item);
                });
            }

        }

        private void BtnClearForms_Click(object sender, RoutedEventArgs e)
        {
            cbDepositPayed.IsChecked = false;
            txtCustomerNumber.Text = "";
            InsertReservationTables();
            dtpReservationTime.setDateTime(DateTime.Now);
            dpReservationDate.SelectedDate = DateTime.Now;
            txtNumOfPersons.Text = "";
            txtReservationComments.Text = "";
        }
    }
}
