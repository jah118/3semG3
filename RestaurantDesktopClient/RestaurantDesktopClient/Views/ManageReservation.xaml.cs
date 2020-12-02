using RestaurantDesktopClient.Views.Controls;
using RestaurantDesktopClient.Views.ManageReservation;
using System.Windows.Controls;

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
            this.DataContext = mrvm;
            ReservationViewControl.dtpReservationTime.DataContext = mrvm;
            ReservationViewControl.DataContext = mrvm;
        }
    }
}
