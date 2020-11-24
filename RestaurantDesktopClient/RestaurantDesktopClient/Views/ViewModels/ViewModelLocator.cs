/*
  In App.xaml:
  <Application.Resources>
      <vm:ViewModelLocator xmlns:vm="clr-namespace:RestaurantDesktopClient"
                           x:Key="Locator" />
  </Application.Resources>
  
  In the View:
  DataContext="{Binding Source={StaticResource Locator}, Path=ViewModelName}"

  You can also use Blend to do all this with the tool's support.
  See http://www.galasoft.ch/mvvm
*/

using CommonServiceLocator;
using DataAccess.DataTransferObjects;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Ioc;
using RestaurantDesktopClient.Reservation;
using RestaurantDesktopClient.Services.CustomerService;
using RestaurantDesktopClient.Services.OrderService;
using RestaurantDesktopClient.Services.Table_Service;

namespace RestaurantDesktopClient.Views.ViewModels
{
    /// <summary>
    /// This class contains static references to all the view models in the
    /// application and provides an entry point for the bindings.
    /// </summary>
    public class ViewModelLocator
    {
        /// <summary>
        /// Initializes a new instance of the ViewModelLocator class.
        /// </summary>
        public ViewModelLocator()
        {
            ServiceLocator.SetLocatorProvider(() => SimpleIoc.Default);

            //if (ViewModelBase.IsInDesignModeStatic)
            //{
            //    // Create design time view services and models
            //    SimpleIoc.Default.Register<IDataService, DesignDataService>();
            //}
            //else
            //{
            //    // Create run time view services and models
            //    SimpleIoc.Default.Register<IDataService, DataService>();
            //}

            SimpleIoc.Default.Register<MainViewModel>();
            SimpleIoc.Default.Register<IRepository<ReservationDTO>, ReservationRepository>();
            SimpleIoc.Default.Register<IRepository<OrderDTO>, OrderRepository>();
            SimpleIoc.Default.Register<IRepository<CustomerDTO>, CustomerRepository>();
            SimpleIoc.Default.Register<IRepository<FoodDTO>, FoodRepository>();
            SimpleIoc.Default.Register<IRepository<TablesDTO>, TableRepository>();
        }

        public MainViewModel Main
        {
            get
            {
                return ServiceLocator.Current.GetInstance<MainViewModel>();
            }
        }
        
        public static void Cleanup()
        {
            // TODO Clear the ViewModels
        }
    }
}