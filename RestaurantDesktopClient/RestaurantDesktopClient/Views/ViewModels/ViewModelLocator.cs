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

using System;
using System.Configuration;
using CommonServiceLocator;
using DataAccess.DataTransferObjects;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Ioc;
using RestaurantDesktopClient.Reservation;
using RestaurantDesktopClient.Services.CustomerService;
using RestaurantDesktopClient.Services.OrderService;
using RestaurantDesktopClient.Services.Table_Service;
using RestaurantDesktopClient.Views.ManageReservation;

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
            try
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
                string constring = ConfigurationManager.ConnectionStrings["ServiceConString"].ConnectionString;
                SimpleIoc.Default.Register<MainMenuViewModel>();
                SimpleIoc.Default.Register<ManageReservationViewModel>();
                SimpleIoc.Default.Register<OrderFoodViewModel>();
                SimpleIoc.Default.Register<IRepository<ReservationDTO>>(() => new ReservationRepository(constring));
                SimpleIoc.Default.Register<IRepository<OrderDTO>>(() => new OrderRepository(constring));
                SimpleIoc.Default.Register<IRepository<CustomerDTO>>(() => new CustomerRepository(constring));
                SimpleIoc.Default.Register<IRepository<FoodDTO>>(() => new FoodRepository(constring));
                SimpleIoc.Default.Register<IRepository<TablesDTO>>(() => new TableRepository(constring));
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }
        }

        public MainMenuViewModel Main => ServiceLocator.Current.GetInstance<MainMenuViewModel>();

        public ManageReservationViewModel ManageReservation => ServiceLocator.Current.GetInstance<ManageReservationViewModel>();

        public OrderFoodViewModel OrderFood => ServiceLocator.Current.GetInstance<OrderFoodViewModel>();

        public static void Cleanup()
        {
            // TODO Clear the ViewModels
        }
    }
}