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
using System.Windows.Controls;

namespace RestaurantDesktopClient.Views.ViewModels
{
    class SummaryFoodsViewModel
    {
        private DataTable _summaryTable;
        private readonly IOrderRepository repository = new OrderRepository();
        private int _reservationId { get; set; }
        public event PropertyChangedEventHandler PropertyChanged;
        public event PropertyChangedEventHandler SelectedPropertyChanged;
        public SummaryFoodsViewModel(int id)
        {
            _reservationId = id;
        }
        public DataTable SearchTable
        {
            get
            {
                List<FoodDTO> foods = (from OrderDTO in repository.GetAllOrders() where OrderDTO.Reservation.Id == _reservationId select OrderDTO).FirstOrDefault().Foods;
                if (foods != null)
                {
                    if (_summaryTable == null) CreateSearchTable();

                    foods.ToList<FoodDTO>().ForEach(x =>
                    {
                        DataRow dr = _summaryTable.NewRow();
                        _summaryTable.Rows.Add(dr.ItemArray = new[] { x.Name, x.Description, x.Price });
                    });
                }
                return _summaryTable;
            }
            set
            {
                _summaryTable = value;
                PropertyChanged(this, new PropertyChangedEventArgs("_foodTable"));
            }
        }
        private void CreateSearchTable()
        {
            _summaryTable = new DataTable();
            _summaryTable.Columns.Add("Name", typeof(String));
            _summaryTable.Columns.Add("Description", typeof(String));
            _summaryTable.Columns.Add("Price", typeof(String));
        }
    }
}
