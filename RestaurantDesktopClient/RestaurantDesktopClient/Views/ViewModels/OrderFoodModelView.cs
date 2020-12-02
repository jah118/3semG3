using DataAccess.DataTransferObjects;
using GalaSoft.MvvmLight.Command;
using RestaurantDesktopClient.Services.OrderService;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RestaurantDesktopClient.Views.ViewModels
{
    class OrderFoodModelView
    {
        private int _reservationId;
        public PaymentCondition SelectedPaymentCondition { get; set; }
        public RelayCommand btnCancelClicked { get; set; }
        public RelayCommand btnSaveClicked { get; set; }
        private ObservableCollection<FoodDTO> _ordersFood;
        public ObservableCollection<FoodDTO> SummaryFoods
        {
            get
            {
                return _ordersFood;
            }
            set { }
        }
        public List<FoodDTO> FoodSearchList
        {
            get
            {
                return _foodRepository.GetAll()
                    .Where(x => x.foodCategoryName.Equals("Mad")).ToList();
            }
            set { }
        }
        public List<FoodDTO> DrinkSearchList
        {
            get
            {
                return _foodRepository.GetAll()
                    .Where(x => x.foodCategoryName.Equals("Drikkevare")).ToList();
            }
            set { }
        }
        private IRepository<FoodDTO> _foodRepository;
        private IRepository<OrderDTO> _orderRepository;
        public OrderFoodModelView(int reservationId)
        {
            _reservationId = reservationId;
            btnCancelClicked = new RelayCommand(CancelClicked);
            btnSaveClicked = new RelayCommand(SaveClicked);
            _foodRepository = new FoodRepository();
            _orderRepository = new OrderRepository();
            var order = _orderRepository.GetAll()
                .Where(x => x.ReservationID == reservationId)
                .OrderBy(x => x.OrderDate)
                .FirstOrDefault();
            _ordersFood = order != null ? new ObservableCollection<FoodDTO>(order.Foods) : new ObservableCollection<FoodDTO>();
            if(order != null)
            {
                SelectedPaymentCondition = (PaymentCondition)Enum.Parse(typeof(PaymentCondition), order.PaymentCondition);
            }

        }
        private void CancelClicked()
        {
            MainWindow.ChangeFrame(new ManageReservationView());
        }
        private void SaveClicked()
        {
            _orderRepository.Create(new OrderDTO()
            {
                Employee = new EmployeeDTO() { Id = 2 }, //TODO change when login are ready
                Foods = _ordersFood.ToList(),
                OrderDate = DateTime.Now,
                ReservationID = _reservationId,
                PaymentCondition = SelectedPaymentCondition.ToString(),
            }) ;
            MainWindow.ChangeFrame(new ManageReservationView());
        }
        private void addToSummary(FoodDTO obj)
        {
            if (!SummaryFoods.Contains(obj))
            {
                obj.Quantity++;
                SummaryFoods.Add(obj);
            }
            else
            {
                obj.Quantity++;
            }

        }
        private void RemoveFromSummary(FoodDTO obj)
        {
            if (obj.Quantity > 1)
            {
                obj.Quantity--;
            }
            else
            {
                obj.Quantity = 0;
                SummaryFoods.Remove(obj);
            }
        }
        public FoodDTO SelectedFood
        {
            get { return null;}
            set
            {
                addToSummary(value);
            }
        }
        public FoodDTO SelectedDrink
        {
            get { return null;}
            set
            {
                addToSummary(value);
            }
        }
        public FoodDTO SelectedSummaryFood
        {
            get { return null;}
            set
            {
                RemoveFromSummary(value);
            }
        }
    }
}
