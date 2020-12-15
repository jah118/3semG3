using DataAccess.DataTransferObjects;
using RestaurantDesktopClient.Services.CustomerService;
using RestaurantDesktopClient.Services.Table_Service;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Net;
using System.Runtime.CompilerServices;
using System.Windows;
using RestaurantDesktopClient.DataTransferObject;
using RestaurantDesktopClient.Reservation;
using RestaurantDesktopClient.Services;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.CommandWpf;
using GalaSoft.MvvmLight.Messaging;
using RestaurantDesktopClient.Helpers;
using RestaurantDesktopClient.Messages;

namespace RestaurantDesktopClient.Views.ManageReservation
{
    public class ManageReservationViewModel : ViewModelBase
    {
        #region Fields
        private readonly IRepository<ReservationDTO> _reservationRepository;
        private readonly IRepository<OrderDTO> _orderRepository;
        private ITableRepository _tableRepository;
        private readonly IRepository<CustomerDTO> _customerRepository;
        private ObservableCollection<TablesDTO> _selectedReservationTables = new ObservableCollection<TablesDTO>();
        private List<ReservationDTO> _reservationSearchList;
        private static ReservationDTO _selectedReservation;
        #endregion
        #region Properties
        /// <summary>
        /// Selected table in current AvailableTables
        /// </summary>
        public TablesDTO AvailableTablesSelected
        {
            get { return null; }
            set
            {
                if (ReservationTables.Where(x => x.Id == value.Id).Count() < 1) ReservationTables.Add((value));
            }
        }
        /// <summary>
        /// Contains available tables of current settings
        /// </summary>
        public ObservableCollection<TablesDTO> AvailableTables { get; set; }
        /// <summary>
        /// Property for this modelview's headline
        /// </summary>
        public string Headline { get { return "Reservationer"; } }
        /// <summary>
        /// Property for SelectedReservation, if SelectedReservation == null new Reservation returned
        /// </summary>
        public ReservationDTO SelectedReservation
        {
            get { return _selectedReservation ?? (_selectedReservation = new ReservationDTO()); }
            set { if (value != _selectedReservation && value != null) UpdateSelectedReservation(value); }
        }
        /// <summary>
        /// Property of SelectedReservation TimeDate
        /// </summary>
        public DateTime GetReservationTimeDate
        {
            get => SelectedReservation != null ? SelectedReservation.ReservationTime : DateTime.Now;
            set
            {
                if (SelectedReservation != null)
                {
                    SelectedReservation.ReservationTime = value + SelectedReservation.ReservationTime.TimeOfDay;
                    ChangePropertyTime();
                };
            }
        }
        /// <summary>
        /// Property for SelectedReservation's reservationtime minuts
        /// </summary>
        public string GetReservationTimeMinuts { get => SelectedReservation != null ? TrimDateTime(SelectedReservation.ReservationTime).Minute + "" : "0"; set { } }
        /// <summary>
        /// Property for SelectedReservation's reservationtime hour
        /// </summary>
        public string GetReservationTimeHours { get => SelectedReservation != null ? SelectedReservation.ReservationTime.Hour + "" : "0"; set { } }
        /// <summary>
        /// Property for Selected table of lvTableNames
        /// </summary>
        public TablesDTO SetSelectedTables
        {
            get
            {
                return SelectedReservation.Tables.FirstOrDefault();
            }
            set
            {
                if (value != null)
                {
                    ReservationTables.Remove(value);
                }

            }
        }
        /// <summary>
        /// Property for SelectedReservation Comments
        /// </summary>
        public string ReservationComment
        {
            get { return SelectedReservation != null ? SelectedReservation.Note : ""; }
            set
            {
                if (SelectedReservation != null)
                {
                    SelectedReservation.Note = value;
                }
            }
        }
        /// <summary>
        /// Property for SelectedReservation's reservation number
        /// </summary>
        public string ReservationNumber
        {
            get { return SelectedReservation != null ? SelectedReservation.Id + "" : ""; }
            set
            {
                if (SelectedReservation != null)
                {
                    int.TryParse(value, out int id);
                    SelectedReservation.Id = id;
                }
            }
        }
        /// <summary>
        /// List for Selected tables
        /// </summary>
        public ObservableCollection<TablesDTO> ReservationTables
        {
            get
            {
                return _selectedReservationTables;
            }
            set
            {
                if (value != null)
                {
                    _selectedReservationTables = value;
                }
            }
        }
        /// <summary>
        /// Property for selected reservations numberOfPeople
        /// </summary>
        public string ReservationNumOfPersons
        {
            get { return SelectedReservation != null ? SelectedReservation.NoOfPeople + "" : ""; }
            set
            {
                if (value != null && Validation.IsNumber(value))
                {
                    SelectedReservation.NoOfPeople = int.Parse(value);
                }
            }
        }
        /// <summary>
        /// Property for selected reservations reservation date
        /// </summary>
        public DateTime ReservationDate
        {
            get { return SelectedReservation != null ? SelectedReservation.ReservationDate : DateTime.Now; }
            set
            {
                if (SelectedReservation != null)
                {
                    SelectedReservation.ReservationDate = value;
                }
            }
        }
        /// <summary>
        /// Property for selected reservations reservation time
        /// </summary>
        public DateTime ReservationTime
        {
            get { return SelectedReservation != null ? SelectedReservation.ReservationTime : DateTime.Now; }
            set
            {
                if (value != null && Validation.IsUpcomingDateTime(value))
                {
                    SelectedReservation.ReservationTime = value;
                }
            }
        }
        /// <summary>
        /// Property for selected reservations Deposit
        /// </summary>
        public bool ReservationDeposit
        {
            get => SelectedReservation != null && SelectedReservation.Deposit;
            set
            {
                if (SelectedReservation != null)
                {
                    SelectedReservation.Deposit = value;
                }
            }
        }
        /// <summary>
        /// Property for Customer id of selectedReservation, Set Customer by Id
        /// </summary>
        public string ReservationCustomer
        {
            get { return SelectedReservation.Customer != null ? SelectedReservation.Customer.Id + "" : ""; }
            set
            {
                if (SelectedReservation != null)
                {
                    if (Validation.IsNumber(value))
                    {
                        var id = int.Parse(value);
                        var customer = _customerRepository.Get(id);
                        if (customer != null)
                        {
                            SelectedReservation.Customer = customer;
                        }
                        else
                        {
                            MessageBox.Show("kunde ikke fundet med dette id");
                        }
                    }
                }
            }
        }
        /// <summary>
        /// Property for Search list of reservations
        /// </summary>
        public List<ReservationDTO> ReservationSearchList
        {
            get
            {
                return _reservationRepository.GetAll().OrderByDescending(dto => dto.ReservationTime).ToList();
            }
            set
            {
                _reservationSearchList = value;
            }
        }

        #endregion
        #region relayCommands
        public RelayCommand CreateReservationCommand { get; set; }
        public RelayCommand RemoveReservationCommand { get; set; }
        public RelayCommand UpdateReservationCommand { get; set; }
        public RelayCommand OrderFoodCommand { get; set; }
        public RelayCommand ReservationTimeAddHours { get; set; }
        public RelayCommand ReservationTimeMinHours { get; set; }
        public RelayCommand ReservationTimeAddMin { get; set; }
        public RelayCommand ReservationTimeMinMinuts { get; set; }
        public RelayCommand ClearValuesCommand { get; set; }
        #endregion
        /// <summary>
        /// Constructor for ManageReservationViewModel
        /// </summary>
        public ManageReservationViewModel(IRepository<ReservationDTO> reservationRepository, IRepository<CustomerDTO> customerRepository
        , ITableRepository tableRepository, IRepository<OrderDTO> orderRepository)
        {
            _orderRepository = orderRepository;
            _customerRepository = customerRepository;
            _tableRepository = tableRepository;
            _reservationRepository = reservationRepository;
            InitRelayCommands();
            UpdateAvailableTables();
            ClearValues();
        }
        /// <summary>
        /// Method for initialise all RelayCommands for ManageReservationViewModel
        /// </summary>
        private void InitRelayCommands()
        {
            ReservationTimeAddHours = new RelayCommand(AddHoursToReservationTime);
            ReservationTimeMinHours = new RelayCommand(MinusHoursFromReservationTime);
            ReservationTimeAddMin = new RelayCommand(AddMinutsToReservationTime);
            ReservationTimeMinMinuts = new RelayCommand(MinMinutsFromReservationTime);
            CreateReservationCommand = new RelayCommand(CreateAndExitReservation);
            RemoveReservationCommand = new RelayCommand(RemoveReservation);
            UpdateReservationCommand = new RelayCommand(UpdateReservation);
            OrderFoodCommand = new RelayCommand(OrderFood);
            ClearValuesCommand = new RelayCommand(ClearValues);
        }
        #region manageReservationControlBindings
        /// <summary>
        /// Updateing List off free tables and clearing current reservationtables
        /// </summary>
        private void UpdateAvailableTables()
        {
            var tempTables = _tableRepository.GetFreeTables(SelectedReservation.ReservationTime);
            AvailableTables = new ObservableCollection<TablesDTO>(tempTables);
            RaisePropertyChanged(() => AvailableTables);
        }
        /// <summary>
        /// Subtract one hour from SelectedReservation reservation time
        /// </summary>
        private void MinusHoursFromReservationTime()
        {
            if (SelectedReservation != null) SelectedReservation.ReservationTime = TrimDateTime(SelectedReservation.ReservationTime.AddHours(-1));
            ChangePropertyTime();
        }
        /// <summary>
        /// Add one hour to SelectedReservation reservation time
        /// </summary>
        private void AddHoursToReservationTime()
        {
            if (SelectedReservation != null) SelectedReservation.ReservationTime = TrimDateTime(SelectedReservation.ReservationTime.AddHours(1));
            ChangePropertyTime();

        }
        /// <summary>
        /// Add 15 minuts to SelectedReservation reservation time
        /// </summary>
        private void AddMinutsToReservationTime()
        {
            if (SelectedReservation != null) SelectedReservation.ReservationTime = TrimDateTime(SelectedReservation.ReservationTime.AddMinutes(15));
            ChangePropertyTime();
        }
        /// <summary>
        /// Subtract 15 min from SelectedReservation's reservation time
        /// </summary>
        private void MinMinutsFromReservationTime()
        {
            if (SelectedReservation != null) SelectedReservation.ReservationTime = TrimDateTime(SelectedReservation.ReservationTime.AddMinutes(-15));
            ChangePropertyTime();
        }
        /// <summary>
        /// Executes RaisePropertyChanged on related properties and execute UpdateAvailableTables
        /// </summary>
        private void ChangePropertyTime(bool clearSelectedTables = true)
        {
            RaisePropertyChanged(() => GetReservationTimeMinuts);
            RaisePropertyChanged(() => GetReservationTimeHours);
            RaisePropertyChanged(() => GetReservationTimeDate);
            UpdateAvailableTables();
            if (clearSelectedTables)
            {
                ReservationTables.Clear();
                RaisePropertyChanged(() => ReservationTables);
            }
        }
        /// <summary>
        /// Trims DateTime to next quartar
        /// </summary>
        /// <param name="DateTime to trim"></param>
        /// <returns>Returns trimmed DateTime eks. 14:09=>14:15</returns>
        private DateTime TrimDateTime(DateTime dt)
        {
            var d = TimeSpan.FromMinutes(15);
            var res = new DateTime((dt.Ticks + d.Ticks - 1) / d.Ticks * d.Ticks, dt.Kind);
            return res;
        }
        /// <summary>
        /// Updateing information on SelectedReservatio in datasource
        /// </summary>
        public void UpdateReservation()
        {
            if (Validation.ReservationValidForUpdate(SelectedReservation))
            {
                var res = _reservationRepository.Update(SelectedReservation);
                if (res == null || res.Id == 0) MessageBox.Show("Fejl ved updatering af reservation");
                else
                {
                    UpdateSelectedReservation(res);
                    RaisePropertyChanged(() => ReservationSearchList);
                }
            }
        }
        /// <summary>
        /// Removes SelectedReservation from database
        /// </summary>
        public void RemoveReservation()
        {
            if (Validation.ReservationValidForDelete(SelectedReservation, _orderRepository))
            {
                var message = "Vil du slette reservation med id: " + SelectedReservation.Id +
                              " som er reserveret til " + SelectedReservation.Customer.FullName +
                              " klokken " + SelectedReservation.ReservationTime.ToString("g") + " ?";
                var result = MessageBox.Show(message, "Advarsel", MessageBoxButton.YesNo);

                if (result == MessageBoxResult.Yes)
                {
                    var res = _reservationRepository.Delete(SelectedReservation);
                    if (res == HttpStatusCode.OK)
                    {
                        ClearValues();
                        RaisePropertyChanged(() => ReservationSearchList);
                    }
                    else if (res == HttpStatusCode.MethodNotAllowed)
                    {
                        MessageBox.Show("Du har ikke tilladelse til at slette en reservation");
                    }
                }
            }

        }
        /// <summary>
        /// Create Selectedreservation if not created, then opens view for orderfood
        /// </summary>
        public void OrderFood()
        {
            if (SelectedReservation.Id == 0)
            {
                ReservationDTO _reservation = CreateReservation();
                if (_reservation == null || _reservation.Id == 0) return;
            }
            var orderFoodVieModel = new OrderFood();
            MainWindow.ChangeFrame(orderFoodVieModel);
            var message = new ReservationSelection() { Selected = _selectedReservation.Id };
            Messenger.Default.Send(message);
        }
        /// <summary>
        /// Updateing SelectedReservation
        /// </summary>
        /// <param name="reservation" that you wish to set to selectedReservation></param>
        private void UpdateSelectedReservation(ReservationDTO reservation)
        {
            var message = new ReservationSelection { Selected = reservation.Id };
            _selectedReservation = reservation;
            Messenger.Default.Send(message);
            ChangePropertyReservation();
            ReservationTables = new ObservableCollection<TablesDTO>(reservation.Tables);
            RaisePropertyChanged(() => ReservationTables);
        }
        /// <summary>
        /// Create Reservation method without return type
        /// </summary>
        public void CreateAndExitReservation()
        {
            CreateReservation();
        }
        /// <summary>
        /// Method for create reservation, Calling IRepository's create method with selectedReservation as parameter
        /// </summary>
        /// <returns>return created reservation with Id if create success, if failed returning SelectedReservation</returns>
        public ReservationDTO CreateReservation()
        {
            ReservationDTO res = null;
            SelectedReservation.Tables = ReservationTables.ToList();
            if (Validation.ReservationValidForCreate(SelectedReservation))
            {
                res = _reservationRepository.Create(SelectedReservation);
                if (res == null) MessageBox.Show("Fejl ved oprettelse af reservation");
                UpdateSelectedReservation(res);
                RaisePropertyChanged(() => ReservationSearchList);
            }

            return res;
        }
        /// <summary>
        /// Clear values from reservation controls
        /// </summary>
        private void ClearValues()
        {
            UpdateSelectedReservation(new ReservationDTO());
        }

        private void ChangePropertyReservation()
        {
            RaisePropertyChanged(() => ReservationNumber);
            RaisePropertyChanged(() => ReservationNumOfPersons);
            RaisePropertyChanged(() => ReservationDate);
            RaisePropertyChanged(() => ReservationDeposit);
            RaisePropertyChanged(() => ReservationCustomer);
            RaisePropertyChanged(() => ReservationComment);
            RaisePropertyChanged(() => ReservationTables);
            ChangePropertyTime(false);

        }
        #endregion
    }
}
