using RestaurantWebApp.DataTransferObject;
using System;
using System.Collections.Generic;

namespace RestaurantWebApp.Service.Interfaces
{
    public interface ITableService : IService<RestaurantTablesDTO>
    {
        IEnumerable<RestaurantTablesDTO> GetTablesByDateTime(DateTime dateTime);
    }
}