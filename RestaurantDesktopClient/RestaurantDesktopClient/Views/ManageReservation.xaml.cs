using DataAccess.DataTransferObjects;
using RestaurantDesktopClient.Controllers;
using RestaurantDesktopClient.Reservation;
using RestaurantDesktopClient.Views.Controls;
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

namespace RestaurantDesktopClient.Views
{
    /// <summary>
    /// Interaction logic for ManageReservation.xaml
    /// </summary>
    public partial class ManageReservationView : Page
    {
        public ManageReservationView()
        {
            InitializeComponent();
            ReservationViewControl.btnCreateNew.Click += BtnCreateNew_Click;
            ListPageControl.dgResult.SelectedCellsChanged += DgResult_SelectedCellsChanged;
        }

        private void DgResult_SelectedCellsChanged(object sender, SelectedCellsChangedEventArgs e)
        {
            DataRowView dr = (DataRowView)ListPageControl.dgResult.SelectedItem;
            int id = int.Parse(dr["Reservation number"].ToString());
            ReservationController rc = new ReservationController();
            ReservationDTO reservation = rc.GetReservationById(id);
            ReservationViewControl.SetReservationInformation(reservation);
        }

        private void BtnCreateNew_Click(object sender, RoutedEventArgs e)
        {
            ReservationDTO reservation = new ReservationDTO();
            reservation.Deposit = (bool)ReservationViewControl.cbDepositPayed.IsChecked;
            reservation.Customer = new CustomerController().getCustomerByid(int.Parse(ReservationViewControl.txtCustomerNumber.Text));
            reservation.RestaurantTable = GetTablesFromlvTableNames();
            reservation.ReservationTime = ReservationViewControl.dtpReservationTime.getDateTime();
            reservation.ReservationDate = ReservationViewControl.dpReservationDate.DisplayDate;
            reservation.NoOfPeople = int.Parse(ReservationViewControl.txtNumOfPersons.Text);
            reservation.Note = ReservationViewControl.txtReservationComments.Text;
            IReservationRepository repository = new ReservationRepository();
            repository.CreateReservation(reservation);
        }
        private List<RestaurantTablesDTO> GetTablesFromlvTableNames()
        {
            List<RestaurantTablesDTO> res = new List<RestaurantTablesDTO>();
            TableController tc = new TableController();
            foreach(string s in ReservationViewControl.lvTableNames.Items)
            {
                res.Add(tc.getTableByNumber(int.Parse(s)));
            }
            return res;
        }
    }
}
