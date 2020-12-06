using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using MvvmCross.Commands;
using MvvmCross.Navigation;
using MvvmCross.ViewModels;
using RestaurantClientService.DataTransferObjects;
using RestaurantClientService.Services;
using RestaurantClientService.Services.FoodsService;
using RestaurantClientService.Services.OrderService;

namespace RestaurantClientService.ViewModels
{
    public class OrderFoodViewModel : MvxViewModel<ReservationDTO>
    {
        #region Fields
        private readonly IMvxNavigationService _navigation;
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
        #region Commands
        public IMvxCommand BtnCancelClicked { get; set; }
        public IMvxCommand BtnSaveClicked { get; set; }

        #endregion

        public OrderFoodViewModel(IMvxNavigationService navigation,
            FoodRepository foodRepository,
            OrderRepository orderRepository)
        {
            _navigation = navigation;
            BtnCancelClicked = new MvxCommand(CancelClicked);
            BtnSaveClicked = new MvxCommand(SaveClicked);
            _foodRepository = foodRepository;
            _orderRepository = orderRepository;
        }

        public override void Prepare(ReservationDTO parameter)
        {
            //Todo make sure concurrency is gooch
            _reservationId = parameter.Id;
            var order = _orderRepository.GetAll()
                .Where(x => x.ReservationID == _reservationId)
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
            _navigation.Navigate<ManageReservationViewModel>();
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
            _navigation.Navigate<ManageReservationViewModel>();
        }
        private void AddToSummary(FoodDTO obj)
        {
            var found = SummaryFoods.Where(x => x.Food.Id == obj.Id).FirstOrDefault();
            if (found == null)
            {
                SummaryFoods.Add(new OrderLineDTO{  Food = obj, Quantity = 1 });
                RaisePropertyChanged(() => SummaryFoods);
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
