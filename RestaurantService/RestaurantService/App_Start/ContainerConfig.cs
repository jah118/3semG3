using Autofac;
using Autofac.Integration.WebApi;
using DataAccess;
using DataAccess.DataTransferObjects;
using DataAccess.Repositories;
using Microsoft.EntityFrameworkCore;
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
            var optionsBuilder = new DbContextOptionsBuilder<RestaurantContext>();
            optionsBuilder.UseSqlServer(_connectionString);
            //pass connectionString, as SQLConnection will not create extra connectionpools if one with the same connectionString already exists
            builder.Register(c => new CustomerRepository(new RestaurantContext(optionsBuilder.Options))).As<IRepository<CustomerDTO>>();
            builder.RegisterApiControllers(Assembly.GetExecutingAssembly());

            var container = builder.Build();
            var resolver = new AutofacWebApiDependencyResolver(container);
            //config.DependencyResolver = resolver;
            GlobalConfiguration.Configuration.DependencyResolver = resolver;
        }
    }
}