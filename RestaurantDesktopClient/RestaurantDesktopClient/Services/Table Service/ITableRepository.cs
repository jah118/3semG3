using DataAccess.DataTransferObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RestaurantDesktopClient.Services.Table_Service
{
    interface ITableRepository
    {
        RestaurantTablesDTO getTableByNumber(int number);
        List<RestaurantTablesDTO> GetAllTables();
    }
}
