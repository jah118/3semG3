using DataAccess.DataTransferObjects;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RestaurantDesktopClient.Views.ViewModels
{
    class SearchDriksViewModel
    {
        private DataTable _foodTable;
        private readonly IFoodRepository repository = new FoodRepository();

        public SearchDriksViewModel()
        {
        }

        private void CreateSearchTable()
        {
            _foodTable = new DataTable();
            _foodTable.Columns.Add("Name", typeof(String));
            _foodTable.Columns.Add("Description", typeof(String));
            _foodTable.Columns.Add("Price", typeof(String));
        }

        public event PropertyChangedEventHandler PropertyChanged;
        public DataTable SearchTable
        {
            get
            {
                List<FoodDTO> foods = repository.GetAllFoods();
                if (foods != null)
                {
                    if (_foodTable == null) CreateSearchTable();

                    var sorted = from FoodDTO in foods.AsEnumerable() where FoodDTO.FoodCategory.Equals("Drink") select FoodDTO;
                    sorted.ToList<FoodDTO>().ForEach(x =>
                    {
                        DataRow dr = _foodTable.NewRow();
                        _foodTable.Rows.Add(dr.ItemArray = new[] { x.Name, x.Description, x.Price });
                    });
                }
                return _foodTable;
            }
            set
            {
                _foodTable = value;
                PropertyChanged(this, new PropertyChangedEventArgs("_foodTable"));
            }
        }
    }
}
