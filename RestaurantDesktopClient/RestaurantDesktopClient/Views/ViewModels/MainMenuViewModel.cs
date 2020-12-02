using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RestaurantDesktopClient.Views.ViewModels
{
    class MainMenuViewModel
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
