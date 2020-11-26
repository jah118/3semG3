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
            builder.Register(c => new BookingServices(ConfigurationManager.AppSettings["ServiceApi"])).As<IService<ReservationDTO>>();
            builder.Register(c => new TableServices(ConfigurationManager.AppSettings["ServiceApi"])).As<IService<RestaurantTablesDTO>>();

            var container = builder.Build();
            DependencyResolver.SetResolver(new AutofacDependencyResolver(container));
      
        }
    }
}