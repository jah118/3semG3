using MvvmCross.Commands;
using MvvmCross.Navigation;
using MvvmCross.ViewModels;

namespace RestaurantClientService.ViewModels
{
    public class MainMenuViewModel : MvxViewModel
    {
        private IMvxNavigationService _navigation;
        public IMvxCommand BtnManageReservationClicked { get; set; }
        public MainMenuViewModel(IMvxNavigationService navigation)
        {
            _navigation = navigation;
            BtnManageReservationClicked = new MvxCommand(ManageReservation_Clicked);
        }

        private void ManageReservation_Clicked()
        {
            _navigation.Navigate()
            MainWindow.ChangeFrame(new ManageReservationView());
        }
    }
}
