using DataAccess.DataTransferObjects;
using RestaurantDesktopClient.Reservation;
using RestaurantDesktopClient.Services.CustomerService;
using RestaurantDesktopClient.Views.Controls;
using RestaurantDesktopClient.Views.ManageReservation;
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
            ManageReservationViewModel mrvm = new ManageReservationViewModel();
            Headline.DataContext = mrvm;
            ListPageControl.DataContext = mrvm;
            ReservationViewControl.dtpReservationTime.DataContext = mrvm;
            ReservationViewControl.DataContext = mrvm;
        }

    }
}
