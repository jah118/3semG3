using Autofac;
using Autofac.Integration.Mvc;
using RestaurantWebApp.Service;
using RestaurantWebApp.Service.Interfaces;
using System.Configuration;
using System.Web.Mvc;

namespace RestaurantWebApp
{
    public class ContainerConfig
    {
        public static void RegisterContainers()
        {
            var builder = new ContainerBuilder();
            builder.RegisterControllers(typeof(MvcApplication).Assembly);
            builder.RegisterType<ReservationService>().As<IReservationService>().SingleInstance();
            builder.RegisterType<TableServices>().As<ITableService>().SingleInstance();
            builder.RegisterType<FoodService>().As<IFoodService>().SingleInstance();
            builder.RegisterType<OrderService>().As<IOrderService>().SingleInstance();
            builder.Register(c => new ReservationService(ConfigurationManager.AppSettings["ServiceApi"])).As<IReservationService>();
            builder.Register(c => new TableServices(ConfigurationManager.AppSettings["ServiceApi"])).As<ITableService>();
            builder.Register(c => new FoodService(ConfigurationManager.AppSettings["ServiceApi"])).As<IFoodService>();
            builder.Register(c => new OrderService(ConfigurationManager.AppSettings["ServiceApi"])).As<IOrderService>();

            var container = builder.Build();
            DependencyResolver.SetResolver(new AutofacDependencyResolver(container));
        }
    }
}