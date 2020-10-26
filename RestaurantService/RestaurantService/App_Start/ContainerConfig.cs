using Autofac;
using Autofac.Integration.WebApi;
using DataAccess;
using DataAccess.Models;
using DataAccess.Repositories;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Reflection;
using System.Web;
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

            builder.RegisterApiControllers(Assembly.GetExecutingAssembly());
            builder.Register(c => new CustomerRepository(_connectionString)).As<IRepository<Customer>>();

            var container = builder.Build();

            config.DependencyResolver = new AutofacWebApiDependencyResolver(container);
        }
    }
}