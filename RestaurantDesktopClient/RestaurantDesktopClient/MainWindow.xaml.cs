using RestaurantDesktopClient.Views;
using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;

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
            ChangeFrame(new LoginView());
        }
        public static void ChangeFrame(Page page)
        {
            mainFrame.Navigate(page);
        }
    }
}
