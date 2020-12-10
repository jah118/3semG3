using DataAccess.DataTransferObjects;
using RestaurantDesktopClient.Reservation;
using RestaurantDesktopClient.Services.CustomerService;
using RestaurantDesktopClient.Services.Table_Service;
using RestaurantDesktopClient.Views.ViewModels;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Windows;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.CommandWpf;
using GalaSoft.MvvmLight.Messaging;
using RestaurantDesktopClient.Messages;
using RestaurantDesktopClient.Services;
using RestaurantDesktopClient.Helpers;

namespace RestaurantDesktopClient.Views.ManageReservation
{
    public class ManageReservationViewModel : ViewModelBase
    {
        #region Fields
        private ITableRepository<TablesDTO> _tablesRepository = new TableRepository();
        private ObservableCollection<TablesDTO> _selectedReservationTables = new ObservableCollection<TablesDTO>();
        private static ReservationDTO _selectedReservation;
        private readonly IRepository<ReservationDTO> _reservationRepository;
        private readonly IRepository<CustomerDTO> _customerRepository;
        private readonly IRepository<TablesDTO> _tableRepository;
        #endregion
        #region Properties
        public TablesDTO AvailableTablesSelected
        {
            get { return null; }
            set
            {
                if (ReservationTables.Where(x => x.Id == value.Id).Count() < 1) ReservationTables.Add((value));
            }
        }
        public ObservableCollection<TablesDTO> AvailableTables { get; set; }
        public string Headline { get { return "Reservationer"; } }
        public TablesDTO SelectedTables { get; set; }
        public ReservationDTO SelectedReservation
        {
            get { return _selectedReservation != null ? _selectedReservation : _selectedReservation = new ReservationDTO(); }
            set { if (value != _selectedReservation && value != null) UpdateSelectedReservation(value); }
        }
        public DateTime GetReservationTimeDate
        {
            get => SelectedReservation != null ? SelectedReservation.ReservationTime : DateTime.Now;
            set
            {
                if (SelectedReservation != null)
                {
                    var newValue = TrimDateTime(value + SelectedReservation.ReservationTime.TimeOfDay);
                    if (Validation.IsUpcomingDateTime(newValue)) SelectedReservation.ReservationTime = newValue;
                };
            }
        }
        public string GetReservationTimeMinuts { get => SelectedReservation != null ? TrimDateTime(SelectedReservation.ReservationTime).Minute + "" : "0"; set { } }
        public string GetReservationTimeHours { get => SelectedReservation != null ? SelectedReservation.ReservationTime.Hour + "" : "0"; set { } }
        public TablesDTO SetSelectedTables
        {
            get
            {
                return SelectedReservation.Tables != null ? SelectedReservation.Tables.FirstOrDefault() ?? null : null;
            }
            set
            {
                if (value != null)
                {
                    ReservationTables.Remove(value);
                }

            }
        }
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
        public string ReservationNumOfPersons
        {
            get { return SelectedReservation != null ? SelectedReservation.NoOfPeople + "" : ""; }
            set
            {
                if (Validation.IsNumber(value))
                {
                    SelectedReservation.NoOfPeople = int.Parse(value);
                    UpdateAvailableTables();
                }
            }
        }
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
        //public DateTime ReservationTime
        //{
        //    get { return SelectedReservation != null ? SelectedReservation.ReservationTime : DateTime.Now; }
        //    set
        //    {
        //        if (SelectedReservation != null && Validation.IsUpcomingDateTime(value))
        //        {
        //            SelectedReservation.ReservationTime = value;
        //        }
        //    }
        //}
        public bool ReservationDeposit
        {
            get => SelectedReservation != null ? SelectedReservation.Deposit : false;
            set
            {
                if (SelectedReservation != null)
                {
                    SelectedReservation.Deposit = value;
                }
            }
        }
        public string ReservationCustomer
        {
            get { return SelectedReservation.Customer != null ? SelectedReservation.Customer.Id + "" : ""; }
            set
            {
                if (Validation.IsNumber(value))
                {
                    var tempCustomer = _customerRepository.Get(int.Parse(value));
                    if (tempCustomer != null)
                    {
                        SelectedReservation.Customer = tempCustomer;
                    }
                    else
                    {
                        MessageBox.Show("Kunden blev ikke fundet");
                    }
                }
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

        public ManageReservationViewModel(IRepository<ReservationDTO> reservationRepository, IRepository<CustomerDTO> customerRepository
        , IRepository<TablesDTO> tableRepository)
        {
            _customerRepository = customerRepository;
            _tableRepository = tableRepository;
            _reservationRepository = reservationRepository;
            InitRelayCommands();
            UpdateAvailableTables();
            ClearValues();
        }

        private void InitRelayCommands()
        {
            ReservationTimeAddHours = new RelayCommand(AddHoursFromReservationTime);
            ReservationTimeMinHours = new RelayCommand(AddHoursToReservationTime);
            ReservationTimeAddMin = new RelayCommand(AddMinutsToReservationTime);
            ReservationTimeMinMinuts = new RelayCommand(MinMinutsFromReservationTime);
            CreateReservationCommand = new RelayCommand(CreateAndExitReservation);
            RemoveReservationCommand = new RelayCommand(RemoveReservation);
            UpdateReservationCommand = new RelayCommand(UpdateReservation);
            OrderFoodCommand = new RelayCommand(OrderFood);
            ClearValuesCommand = new RelayCommand(ClearValues);
        }


        private List<ReservationDTO> _reservationSearchList;
        public List<ReservationDTO> ReservationSearchList
        {
            get
            {
                return _reservationSearchList ?? _reservationRepository.GetAll().ToList();
            }
            set
            {
                _reservationSearchList = value;
            }
        }

        #region manageReservationControlBindings
        private void UpdateAvailableTables()
        {
            var tempTables = _tablesRepository.GetFreeTables(SelectedReservation.ReservationTime)
                .Where(x => x.NoOfSeats > SelectedReservation.NoOfPeople);
            AvailableTables = new ObservableCollection<TablesDTO>(tempTables);
            ReservationTables.Clear();
            RaisePropertyChanged(() => AvailableTables);
        }
        private void AddHoursToReservationTime()
        {
            var newTime = TrimDateTime(SelectedReservation.ReservationTime.AddHours(-1));
            if (Validation.IsUpcomingDateTime(newTime))
            {
                SelectedReservation.ReservationTime = newTime;
                ChangePropertyTime();
            }
        }
        private void AddHoursFromReservationTime()
        {
            if (SelectedReservation != null) SelectedReservation.ReservationTime = TrimDateTime(SelectedReservation.ReservationTime.AddHours(1));
            ChangePropertyTime();

        }
        private void AddMinutsToReservationTime()
        {
            if (SelectedReservation != null) SelectedReservation.ReservationTime = TrimDateTime(SelectedReservation.ReservationTime.AddMinutes(15));
            ChangePropertyTime();
        }
        private void MinMinutsFromReservationTime()
        {
            var newTime = TrimDateTime(SelectedReservation.ReservationTime.AddMinutes(-15));
            if (Validation.IsUpcomingDateTime(newTime))
            {
                SelectedReservation.ReservationTime = newTime;
                ChangePropertyTime();
            }
        }

        private void ChangePropertyTime()
        {
            RaisePropertyChanged(() => GetReservationTimeMinuts);
            RaisePropertyChanged(() => GetReservationTimeHours);
            RaisePropertyChanged(() => GetReservationTimeDate);
            UpdateAvailableTables();
        }

        private DateTime TrimDateTime(DateTime dt)
        {
            var d = TimeSpan.FromMinutes(15);
            var res = new DateTime((dt.Ticks + d.Ticks - 1) / d.Ticks * d.Ticks, dt.Kind);
            return res;
        }
        public void UpdateReservation()
        {

        }
        public void RemoveReservation()
        {

        }
        public void OrderFood()
        {
            if (SelectedReservation.Id == 0)
            {
                var _reservation = CreateReservation();
                if (_reservation == null || _reservation.Id == 0) return;
                UpdateSelectedReservation(_reservation);
            }
            var orderFoodVieModel = new OrderFood();
            MainWindow.ChangeFrame(orderFoodVieModel);
            var message = new ReservationSelection() { Selected = _selectedReservation.Id };
            Messenger.Default.Send(message);
        }
        private void UpdateSelectedReservation(ReservationDTO reservation)
        {
            var message = new ReservationSelection() { Selected = reservation.Id };
            _selectedReservation = reservation;
            UpdateAvailableTables();
            _selectedReservationTables = new ObservableCollection<TablesDTO>(_selectedReservation.Tables);
            Messenger.Default.Send(message);
            RaisePropertyChanged(string.Empty);
        }
        public void CreateAndExitReservation()
        {
            var newReservation = CreateReservation();
            if(newReservation != null) UpdateSelectedReservation(newReservation);
        }
        public ReservationDTO CreateReservation()
        {
            SelectedReservation.Tables = _selectedReservationTables.ToList();
            if (!Validation.ReservationValidForCreate(SelectedReservation)) return null;
            var res = _reservationRepository.Create(SelectedReservation);
            if (res == null) MessageBox.Show("Fejl ved oprettelse af reservation");
            return res;
        }
        private void ClearValues()
        {
            UpdateSelectedReservation(new ReservationDTO()
            {
                ReservationDate = DateTime.Now,
                ReservationTime = DateTime.Now,
                Deposit = false,
            });
        }
        #endregion
    }
}
