using DataAccess.DataTransferObjects;
using RestaurantDesktopClient.Services.Table_Service;
using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RestaurantDesktopClient.Controllers
{
    class TableController
    {
        ITableRepository repository;
        public TableController()
        {
            repository = new TableRepository();
        }

        public List<RestaurantTablesDTO> getAllTables()
        {
            List<RestaurantTablesDTO> res = repository.GetAllTables();
            return res;
        }

        public RestaurantTablesDTO getTableByNumber(int number)
        {
            RestaurantTablesDTO res = repository.getTableByNumber(number);
            return res;
        }
    }
}
