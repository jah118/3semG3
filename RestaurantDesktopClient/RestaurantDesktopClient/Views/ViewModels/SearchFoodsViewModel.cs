using DataAccess.DataTransferObjects;
using RestaurantDesktopClient.Services.OrderService;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace RestaurantDesktopClient.Views.ViewModels
{
    class SearchFoodsViewModel : INotifyPropertyChanged
    {
        private DataTable _foodTable;
        private readonly IRepository<FoodDTO> repository;
        private DataRowView _selectedRowView;
        public DataRowView SelectedRowView
        {
            get
            {
                return _selectedRowView;
            }
            set
            {
                _selectedRowView = value;
                int.TryParse(_selectedRowView.Row.ItemArray[0].ToString(), out int id);
                OnPropertyChanged("SelectedFood");
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged([CallerMemberName] string name = null)
        {
            this.PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
        }
        public SearchFoodsViewModel()
        {
            repository = new FoodRepository();
        }

        private void CreateSearchTable()
        {
            _foodTable = new DataTable();
            _foodTable.Columns.Add("Name", typeof(String));
            _foodTable.Columns.Add("Price", typeof(String));
            _foodTable.Columns.Add("Description", typeof(String));
        }

        public DataTable SearchTable
        {
            get
            {
                IEnumerable<FoodDTO> foods = repository.GetAll();
                if (foods != null)
                {
                    if (_foodTable == null) CreateSearchTable();

                    var sorted = from FoodDTO in foods.AsEnumerable() where FoodDTO.foodCategoryName.Equals("Mad") select FoodDTO;
                    sorted.ToList<FoodDTO>().ForEach(x =>
                    {
                        DataRow dr = _foodTable.NewRow();
                        _foodTable.Rows.Add(dr.ItemArray = new[]{ x.Name, x.Price.ToString("#.##"), x.Description});
                    });
                }
                return _foodTable;
            }
            set
            {
                _foodTable = value;
                OnPropertyChanged("SearchTable");
            }
        }


    }
}

