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

namespace RestaurantDesktopClient
{
    /// <summary>
    /// Interaction logic for MainMenu.xaml
    /// </summary>
    public partial class MainMenu : Page
    {
        private readonly MainWindow mainWindow;
        public MainMenu(MainWindow mainWindow)
        {
            InitializeComponent();
            this.mainWindow = mainWindow;
        }

        private void BtnManageEmployee_Click(object sender, RoutedEventArgs e)
        {
            mainWindow.MainFrame.Navigate(new ManagePage());
        }

        private void BtnManageCusomter_Click(object sender, RoutedEventArgs e)
        {
           // mainWindow.MainFrame.Navigate(new ManagePage());
        }
    }
}
