using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using Microsoft.Extensions.Configuration;
using MvvmCross;
using MvvmCross.ViewModels;
using RestaurantClientService.DataTransferObjects;
using RestaurantClientService.Services;
using RestaurantClientService.Services.FoodsService;
using RestaurantClientService.ViewModels;

namespace RestaurantClientService
{
    public class App : MvxApplication
    {
        MvxBundle
        public override void Initialize()
        {
            var config = Configure();
            var connection = config.GetSection("connection").Value;
            Mvx.IoCProvider.RegisterType(IRepository<CustomerDTO>, () => connection);
            Mvx.IoCProvider.RegisterType<IRepository<FoodDTO>, FoodRepository>();
            Mvx.IoCProvider.RegisterType<IRepository<FoodDTO>, FoodRepository>();
            Mvx.IoCProvider.RegisterType<IRepository<FoodDTO>, FoodRepository>();
            Mvx.IoCProvider.RegisterType<IRepository<FoodDTO>, FoodRepository>();
            Mvx.IoCProvider.RegisterType<IRepository<FoodDTO>, FoodRepository>();
            RegisterAppStart<MainMenuViewModel>();
            base.Initialize();
        }

        private static IConfiguration Configure()
        {
            var configuration = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("ServiceConfig.json", optional: false, reloadOnChange: true)
                .Build();

            return configuration;
        }
    }
}
