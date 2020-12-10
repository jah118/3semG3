using System;
using System.Collections.Generic;
using DataAccess.DataTransferObjects;
using RestaurantDesktopClient.DataTransferObject;
using RestaurantDesktopClient.Views.ViewModels;

namespace RestaurantDesktopClient.Services.Table_Service
{
    internal interface ITableRepository<T> : IRepository<T>
    {
        AvailableTimesDTO GetReservationTimeByDate(DateTime date);
        List<TablesDTO> GetFreeTables(DateTime date);
    }
}