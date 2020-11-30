using Autofac;
using RestaurantWebApp.Service;
using RestaurantWebApp.Service.Interfaces;
using System.Configuration;
using System.Web.Mvc;
using Autofac.Integration.Mvc;
using RestaurantWebApp.DataTransferObject;

namespace RestaurantWebApp
{
    public class ContainerConfig
    {
        public static void RegisterContainers()
        {
            var builder = new ContainerBuilder();
            builder.RegisterControllers(typeof(MvcApplication).Assembly);
            builder.RegisterType<BookingServices>().As<IBookingService>().SingleInstance();
            builder.RegisterType<TableServices>().As<ITableService>().SingleInstance();
            builder.RegisterType<FoodService>().As<IFoodService>().SingleInstance();
            builder.Register(c => new BookingServices(ConfigurationManager.AppSettings["ServiceApi"])).As<IBookingService>();
            builder.Register(c => new TableServices(ConfigurationManager.AppSettings["ServiceApi"])).As<ITableService>();
            builder.Register(c => new FoodService(ConfigurationManager.AppSettings["ServiceApi"])).As<IFoodService>();

            var container = builder.Build();
            DependencyResolver.SetResolver(new AutofacDependencyResolver(container));
      
        }
    }
}