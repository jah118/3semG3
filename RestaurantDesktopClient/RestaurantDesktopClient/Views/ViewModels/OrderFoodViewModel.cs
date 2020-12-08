using DataAccess.DataTransferObjects;
using RestaurantDesktopClient.DataTransferObject;
using RestaurantDesktopClient.Services.OrderService;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data;
using System.Linq;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.CommandWpf;
using GalaSoft.MvvmLight.Messaging;
using RestaurantDesktopClient.Messages;
using RestaurantDesktopClient.Views.ManageReservation;

namespace RestaurantDesktopClient.Views.ViewModels
{
    public class OrderFoodViewModel : ViewModelBase
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
        private readonly IRepository<FoodDTO> _foodRepository;
        private readonly IRepository<OrderDTO> _orderRepository;
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

        public OrderFoodViewModel(IRepository<FoodDTO> foodRepository, IRepository<OrderDTO> orderRepository)
        {
            Messenger.Default.Register<ReservationSelection>(this, ChangeReservation);
            BtnCancelClicked = new RelayCommand(CancelClicked);
            BtnSaveClicked = new RelayCommand(SaveClicked);
            _foodRepository =foodRepository;
            _orderRepository = orderRepository;
        }

        public void ChangeReservation(ReservationSelection message)
        {
            _reservationId = message.Selected;
            var order = _orderRepository.GetAll()
                .Where(x => x.ReservationID == _reservationId)
                .OrderBy(x => x.OrderDate)
                .FirstOrDefault();
            _ordersFood = order != null ? new ObservableCollection<OrderLineDTO>(order.OrderLines) : new ObservableCollection<OrderLineDTO>();
            if (order != null)
            {
                SelectedPaymentCondition = (PaymentCondition)Enum.Parse(typeof(PaymentCondition), order.PaymentCondition);
            }
            RaisePropertyChanged();
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
            var found = SummaryFoods.FirstOrDefault(x => x.Food.Id == obj.Id);
            if (found == null)
            {
                SummaryFoods.Add(new OrderLineDTO{  Food = obj, Quantity = 1 });
                RaisePropertyChanged(string.Empty);
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
            RaisePropertyChanged(string.Empty);
        }

    }
}
