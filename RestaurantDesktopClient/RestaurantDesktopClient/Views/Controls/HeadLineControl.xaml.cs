﻿using System;
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

namespace RestaurantDesktopClient.Views.Controls
{
    /// <summary>
    /// Interaction logic for HeadLineControl.xaml
    /// </summary>
    public partial class HeadLineControl : UserControl
    {
        public HeadLineControl()
        {
            InitializeComponent();
        }

        internal void SetHeadline(string text)
        {
            lblManageEmployee.Content = text;
        }
    }
}
