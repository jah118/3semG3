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

namespace RestaurantDesktopClient.Views.Controls
{
    /// <summary>
    /// Interaction logic for DateTimePicker.xaml
    /// </summary>
    public partial class DateTimePicker : UserControl
    {
        DateTime time;
        public DateTimePicker()
        {
            InitializeComponent();
            time = new DateTime();
            dpDate.SelectedDateChanged += DpDate_SelectedDateChanged;
            dpDate.SelectedDate = DateTime.Now;
        }

        internal DateTime getDateTime()
        {
            return time;
        }

        private DateTime trimDateTime(DateTime dt)
        {
            if (dt.Minute > 0 && dt.Minute < 15)
            {
                while(dt.Minute < 15)
                {
                    dt = dt.AddMinutes(1);
                }
            }
            else if (dt.Minute > 15 && dt.Minute < 30)
            {
                while (dt.Minute < 30)
                {
                    dt = dt.AddMinutes(1);
                }
            }
            else if (dt.Minute > 30 && dt.Minute < 45)
            {
                while (dt.Minute < 45)
                {
                    dt = dt.AddMinutes(1);
                }
            }
            else if (dt.Minute > 45 && dt.Minute <= 59)
            {
                while (dt.Minute >1)
                {
                    dt = dt.AddMinutes(-1);
                }
            }
            return dt;
        }

        private void DpDate_SelectedDateChanged(object sender, SelectionChangedEventArgs e)
        {
            DateTime temp = new DateTime();
            temp = temp.AddDays(dpDate.SelectedDate.Value.Day - 1);
            temp = temp.AddMonths(dpDate.SelectedDate.Value.Month - 1);
            temp = temp.AddYears(dpDate.SelectedDate.Value.Year - 1);
            temp = temp.AddHours(time.Hour);
            temp = temp.AddMinutes(time.Minute);
            temp = temp.AddSeconds(time.Second);
            time = temp;
        }

        internal void setDateTime(DateTime reservationTime)
        {
            time = trimDateTime(reservationTime);
            dpDate.SelectedDate = time;
            updateTbHoursText();
            updateTbMinText();
        }

        private void btnPlusHours_Click(object sender, RoutedEventArgs e)
        {
            if(time.Hour <= 22)
            {
                time = time.AddHours(1);
            }
            else
            {
                time = time.AddHours(-23);
            }
            updateTbHoursText();
        }

        private void btnMinHours_Click(object sender, RoutedEventArgs e)
        {
            if(time.Hour > 0)
            {
                time = time.AddHours(-1);
            }
            else{
                time = time.AddHours(23);
            }
            updateTbHoursText();
        }
        private void btnPlusMin_Click(object sender, RoutedEventArgs e)
        {
            if(time.Minute <= 30)
            {
                time = time.AddMinutes(15);
            }
            else
            {
                time = time.AddMinutes(-45);
            }
            updateTbMinText();
        }
        private void btnMinMin_Click(object sender, RoutedEventArgs e)
        {
            if (time.Minute >= 15)
            {
                time = time.AddMinutes(-15);
            }
            else
            {
                time = time.AddMinutes(45);
            }
            updateTbMinText();
        }
        private void updateTbHoursText()
        {
            tbHours.Text = time.Hour + "";
        }
        private void updateTbMinText()
        {
            tbMin.Text = time.Minute + "";
        }

    }
}
