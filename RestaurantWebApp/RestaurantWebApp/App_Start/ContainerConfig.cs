using Autofac;
using Autofac.Integration.Mvc;
using RestaurantWebApp.Service;
using RestaurantWebApp.Service.Interfaces;
using System.Configuration;
using System.Web.Mvc;
using RestaurantWebApp.DataTransferObject;

namespace RestaurantWebApp
{
    public class ContainerConfig
    {
        public static void RegisterContainers()
        {
            var builder = new ContainerBuilder();
            builder.RegisterControllers(typeof(MvcApplication).Assembly);
            //builder.RegisterType<AuthorizationService>().As<IAuthService>().SingleInstance();
            //builder.RegisterType<UserDTO>().SingleInstance();
            builder.RegisterType<ReservationService>().As<IReservationService>().SingleInstance();
            builder.RegisterType<TableServices>().As<ITableService>().SingleInstance();
            builder.RegisterType<FoodService>().As<IFoodService>().SingleInstance();
            builder.RegisterType<OrderService>().As<IOrderService>().SingleInstance();

            //builder.RegisterType<CustomerService>().As<IService<CustomerDTO>>().SingleInstance();
            builder.Register(c => new AuthorizationService(ConfigurationManager.AppSettings["ServiceApi"])).As<IAuthService>().SingleInstance();
            builder.Register(c => new CustomerService(ConfigurationManager.AppSettings["ServiceApi"], c.Resolve<IAuthService>())).As<IService<CustomerDTO>>().SingleInstance();
            builder.Register(c => new ReservationService(ConfigurationManager.AppSettings["ServiceApi"], c.Resolve<IAuthService>())).As<IReservationService>().SingleInstance();
            builder.Register(c => new TableServices(ConfigurationManager.AppSettings["ServiceApi"])).As<ITableService>().SingleInstance();
            builder.Register(c => new FoodService(ConfigurationManager.AppSettings["ServiceApi"])).As<IFoodService>().SingleInstance();
            builder.Register(c => new OrderService(ConfigurationManager.AppSettings["ServiceApi"], c.Resolve<IAuthService>())).As<IOrderService>().SingleInstance();


            var container = builder.Build();
            DependencyResolver.SetResolver(new AutofacDependencyResolver(container));
        }
    }
}