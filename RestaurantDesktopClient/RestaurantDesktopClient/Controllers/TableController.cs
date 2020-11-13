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

        public List<TablesDTO> getAllTables()
        {
            List<TablesDTO> res = repository.GetAllTables();
            return res;
        }

        public TablesDTO getTableByNumber(int number)
        {
            TablesDTO res = repository.GetTableByNumber(number);
            return res;
        }
    }
}
