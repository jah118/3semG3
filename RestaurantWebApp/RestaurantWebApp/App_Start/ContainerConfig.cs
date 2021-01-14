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

            builder.Register(c => new AuthorizationService(ConfigurationManager.AppSettings["ServiceApi"])).As<IAuthService>().InstancePerRequest();
            builder.Register(c => new CustomerService(ConfigurationManager.AppSettings["ServiceApi"],
                            c.Resolve<IAuthService>())).As<IService<CustomerDTO>>().InstancePerRequest();
            builder.Register(c => new UserService(ConfigurationManager.AppSettings["ServiceApi"],
                            c.Resolve<IAuthService>())).As<IUserService>().InstancePerRequest();
            builder.Register(c => new ReservationService(ConfigurationManager.AppSettings["ServiceApi"],
                            c.Resolve<IAuthService>())).As<IReservationService>().InstancePerRequest();
            builder.Register(c => new TableServices(ConfigurationManager.AppSettings["ServiceApi"])).As<ITableService>().InstancePerRequest();
            builder.Register(c => new FoodService(ConfigurationManager.AppSettings["ServiceApi"])).As<IFoodService>().InstancePerRequest();
            builder.Register(c => new OrderService(ConfigurationManager.AppSettings["ServiceApi"],
                            c.Resolve<IAuthService>())).As<IOrderService>().InstancePerRequest();


            var container = builder.Build();
            DependencyResolver.SetResolver(new AutofacDependencyResolver(container));
        }
    }
}