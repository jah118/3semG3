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
            builder.RegisterType<AuthorizationService>().As<IAuthService>().SingleInstance();
            builder.RegisterType<ReservationService>().As<IReservationService>().SingleInstance();
            builder.RegisterType<TableServices>().As<ITableService>().SingleInstance();
            builder.RegisterType<FoodService>().As<IFoodService>().SingleInstance();
            builder.RegisterType<OrderService>().As<IOrderService>().SingleInstance();
            builder.RegisterType<CustomerService>().As<IService<CustomerDTO>>().SingleInstance();

            //var output = new AuthorizationService(ConfigurationManager.AppSettings["ServiceApi"]);
            //builder.RegisterInstance(output).As<IAuthService>();
            //builder.Register(c => new AuthorizationService(ConfigurationManager.AppSettings["ServiceApi"])).As<IAuthService>().Named<AuthorizationService>("auth");
            var o = builder.Register(c => new AuthorizationService(ConfigurationManager.AppSettings["ServiceApi"])).As<IAuthService>();
            builder.Register(c=> new CustomerService(ConfigurationManager.AppSettings["ServiceApi"], c.Resolve< IAuthService>())).As<IService<CustomerDTO>>();

            
            //builder.Register((c,p) => new CustomerService(ConfigurationManager.AppSettings["ServiceApi"],p.Named<AuthorizationService>("auth"))).As<IService<CustomerDTO>>();
            //builder.Register((c,p) => new CustomerService(ConfigurationManager.AppSettings["ServiceApi"],p.Named<AuthorizationService>("auth"))).As<IService<CustomerDTO>>();
            builder.Register(c => new ReservationService(ConfigurationManager.AppSettings["ServiceApi"])).As<IReservationService>();
            builder.Register(c => new TableServices(ConfigurationManager.AppSettings["ServiceApi"])).As<ITableService>();
            builder.Register(c => new FoodService(ConfigurationManager.AppSettings["ServiceApi"])).As<IFoodService>();
            builder.Register(c => new OrderService(ConfigurationManager.AppSettings["ServiceApi"])).As<IOrderService>();

            var container = builder.Build();
            DependencyResolver.SetResolver(new AutofacDependencyResolver(container));
        }
    }
}