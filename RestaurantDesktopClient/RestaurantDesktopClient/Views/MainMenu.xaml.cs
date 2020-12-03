using RestaurantDesktopClient.Views;
using System;
using System.Collections.Generic;
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
    public partial class MainMenu : Page
    {
        public MainMenu()
        {
            InitializeComponent();
        }

        private void BtnManageEmployee_Click(object sender, RoutedEventArgs e)
        {
            MainWindow.ChangeFrame(new ManageEmployee());
        }

        private void BtnManageCusomter_Click(object sender, RoutedEventArgs e)
        {
            MainWindow.ChangeFrame(new ManageCustomer());
        }

        private void BtnReservatons_Click(object sender, RoutedEventArgs e)
        {
            MainWindow.ChangeFrame( new ManageReservationView());
        }

        private void BtnManageTables_Click(object sender, RoutedEventArgs e)
        {
            MainWindow.ChangeFrame(new ManageTable());
        }
    }
}
