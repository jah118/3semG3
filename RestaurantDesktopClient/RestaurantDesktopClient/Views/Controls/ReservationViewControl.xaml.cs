using DataAccess.DataTransferObjects;
using RestaurantDesktopClient.Services.Table_Service;
using RestaurantDesktopClient.Views.ViewModels;
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
        public ReservationViewControl()
        {
            InitializeComponent();
        }

        private void BtnClearForms_Click(object sender, RoutedEventArgs e)
        {
            cbDepositPayed.IsChecked = false;
            txtCustomerNumber.Text = "";
            dpReservationDate.SelectedDate = DateTime.Now;
            txtNumOfPersons.Text = "";
            txtReservationComments.Text = "";
            txtReservationNumber.Text = "";
        }
    }
}
