using System;
using System.Collections.Generic;
using DataAccess.DataTransferObjects;
using RestaurantDesktopClient.DataTransferObject;
using RestaurantDesktopClient.Views.ViewModels;

namespace RestaurantDesktopClient.Services.Table_Service
{
    public interface ITableRepository : IRepository<TablesDTO>
    {
        List<TablesDTO> GetFreeTables(DateTime date);
    }
}