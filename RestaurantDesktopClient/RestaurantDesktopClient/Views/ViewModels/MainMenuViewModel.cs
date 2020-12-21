using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.CommandWpf;

namespace RestaurantDesktopClient.Views.ViewModels
{
    public class MainMenuViewModel : ViewModelBase
    {
        public RelayCommand BtnManageReservationClicked { get; set; }

        public MainMenuViewModel()
        {
            BtnManageReservationClicked = new RelayCommand(ManageReservation_Clicked);
        }

        private void ManageReservation_Clicked()
        {
            MainWindow.ChangeFrame(new ManageReservationView());
        }
    }
}
