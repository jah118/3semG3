using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using MvvmCross.ViewModels;
using RestaurantClientService.Services;
using RestaurantClientService.Services.FoodsService;
using RestaurantClientService.Services.OrderService;

namespace RestaurantClientService.ViewModels
{
    public class OrderFoodModelView : MvxViewModel
    {
        #region Fields
        private int _reservationId;
        private ObservableCollection<OrderLineDTO> _ordersFood;
        #endregion
        #region Properties
        public PaymentCondition SelectedPaymentCondition { get; set; }
        public ObservableCollection<OrderLineDTO> SummaryFoods
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
                    .Where(x => x.FoodCategoryName.Equals("Mad")).ToList();
            }
            set { }
        }
        public List<FoodDTO> DrinkSearchList
        {
            get
            {
                return _foodRepository.GetAll()
                    .Where(x => x.FoodCategoryName.Equals("Drikkevare")).ToList();
            }
            set { }
        }
        private IRepository<FoodDTO> _foodRepository;
        private IRepository<OrderDTO> _orderRepository;
        public FoodDTO SelectedFood
        {
            get { return null; }
            set
            {
                AddToSummary(value);
            }
        }
        public FoodDTO SelectedDrink
        {
            get { return null; }
            set
            {
                AddToSummary(value);
            }
        }
        public OrderLineDTO SelectedSummaryFood
        {
            get { return null; }
            set
            {
                RemoveFromSummary(value);
            }
        }
        #endregion
        #region Relaycommand
        public RelayCommand BtnCancelClicked { get; set; }
        public RelayCommand BtnSaveClicked { get; set; }

        #endregion

        public OrderFoodModelView(int reservationId)
        {
            _reservationId = reservationId;
            BtnCancelClicked = new RelayCommand(CancelClicked);
            BtnSaveClicked = new RelayCommand(SaveClicked);
            _foodRepository = new FoodRepository();
            _orderRepository = new OrderRepository();
            var order = _orderRepository.GetAll()
                .Where(x => x.ReservationID == reservationId)
                .OrderBy(x => x.OrderDate)
                .FirstOrDefault();
            _ordersFood = order != null ? new ObservableCollection<OrderLineDTO>(order.OrderLines) : new ObservableCollection<OrderLineDTO>();
            if (order != null)
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
                EmployeeID = 2, //TODO change when login are ready
                OrderLines = _ordersFood.ToList(),
                OrderDate = DateTime.Now,
                ReservationID = _reservationId,
                PaymentCondition = SelectedPaymentCondition.ToString(),
            });
            MainWindow.ChangeFrame(new ManageReservationView());
        }
        private void AddToSummary(FoodDTO obj)
        {
            var found = SummaryFoods.Where(x => x.Food.Id == obj.Id).FirstOrDefault();
            if (found == null)
            {
                SummaryFoods.Add(new OrderLineDTO{  Food = obj, Quantity = 1 });
            }
            else
            {
                found.Quantity++;
            }

        }
        private void RemoveFromSummary(OrderLineDTO obj)
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

    }
}
