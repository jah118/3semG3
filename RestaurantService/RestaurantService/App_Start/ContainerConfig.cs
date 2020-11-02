using Autofac;
using Autofac.Integration.WebApi;
using DataAccess;
using DataAccess.Models;
using DataAccess.Repositories;
using System.Configuration;
using System.Reflection;
using System.Web.Http;

namespace RestaurantService.App_Start
{
    public class ContainerConfig
    {
        public static void RegisterContainers()
        {
            string _connectionString = ConfigurationManager.ConnectionStrings["default"].ConnectionString;
              
            var builder = new ContainerBuilder();
            var config = GlobalConfiguration.Configuration;

            //pass connectionString, as SQLConnection will not create extra connectionpools if one with the same connectionString already exists
            builder.Register(c => new CustomerRepository(_connectionString)).As<IRepository<Customer2>>();
            builder.RegisterApiControllers(Assembly.GetExecutingAssembly());

            var container = builder.Build();
            var resolver = new AutofacWebApiDependencyResolver(container);
            //config.DependencyResolver = resolver;
            GlobalConfiguration.Configuration.DependencyResolver = resolver;
        }
    }
}