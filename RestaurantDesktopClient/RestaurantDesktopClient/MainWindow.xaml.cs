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

namespace RestaurantDesktopClient
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private static Frame mainFrame;
        public MainWindow()
        {
            InitializeComponent();
            mainFrame = MainFrame;
            ChangeFrame(new MainMenu());
        }
        public static void ChangeFrame(Page page)
        {
            mainFrame.Navigate(page);
        }
    }
}
