using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using Microsoft.Extensions.Configuration;
using MvvmCross;
using MvvmCross.IoC;
using MvvmCross.ViewModels;
using RestaurantClientService.DataTransferObjects;
using RestaurantClientService.Services;
using RestaurantClientService.Services.CustomerService;
using RestaurantClientService.Services.FoodsService;
using RestaurantClientService.Services.OrderService;
using RestaurantClientService.Services.ReservationService;
using RestaurantClientService.Services.Table_Service;
using RestaurantClientService.ViewModels;

namespace RestaurantClientService
{
    public class App : MvxApplication
    {
        public override void Initialize()
        {
            var config = Configure();
            var connection = config.GetSection("connection").Value;
            Mvx.IoCProvider.RegisterType<IRepository<CustomerDTO>, CustomerRepository>(repository => new CustomerRepository(connection));
            Mvx.IoCProvider.RegisterType<IRepository<FoodDTO>, FoodRepository>(repository => new FoodRepository(connection));
            Mvx.IoCProvider.RegisterType<IRepository<OrderDTO>, OrderRepository>(repository => new OrderRepository(connection));
            Mvx.IoCProvider.RegisterType<IRepository<ReservationDTO>, ReservationRepository>(repository => new ReservationRepository(connection));
            Mvx.IoCProvider.RegisterType<IRepository<TablesDTO>, TableRepository>(repository => new TableRepository(connection));
            //Mvx.IoCProvider.RegisterType<OrderFoodViewModel>(() => new OrderFoodViewModel());
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
