using System;
using System.Collections.Generic;
using RestaurantWebApp.DataTransferObject;

namespace RestaurantWebApp.Service.Interfaces
{
    public interface ITableService : IService<RestaurantTablesDTO>
    {
        IEnumerable<RestaurantTablesDTO> GetTablesByDateTime(DateTime dateTime);
    }
}