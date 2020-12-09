using DataAccess.DataTransferObjects;
using RestaurantDesktopClient.Services.CustomerService;
using RestaurantDesktopClient.Services.Table_Service;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Windows;
using RestaurantDesktopClient.DataTransferObject;
using RestaurantDesktopClient.Reservation;
using RestaurantDesktopClient.Services;

namespace RestaurantDesktopClient.Views.ManageReservation
{
    class ManageReservationViewModel : INotifyPropertyChanged
    {
        #region Fields
        private static ReservationDTO _selectedReservation;
        private readonly IRepository<ReservationDTO> _reservationRepository = new ReservationRepository();
        private ITableRepository<TablesDTO> _tablesRepository = new Services.Table_Service.TableRepository();
        private ObservableCollection<TablesDTO> _selectedReservationTables = new ObservableCollection<TablesDTO>();
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
        public ReservationDTO SelectedReservation
        {
            get { return _selectedReservation != null ? _selectedReservation : _selectedReservation = new ReservationDTO(); }
            set { UpdateSelectedReservation(value); _selectedReservation = value; }
        }
        public DateTime GetReservationTimeDate
        {
            get => SelectedReservation != null ? SelectedReservation.ReservationTime : DateTime.Now;
            set { if (SelectedReservation != null) { SelectedReservation.ReservationTime = value; UpdateAvailableTables(); }; }
        }
        public string GetReservationTimeMinuts { get => SelectedReservation != null ? TrimDateTime(SelectedReservation.ReservationTime).Minute + "" : "0"; set { } }
        public string GetReservationTimeHours { get => SelectedReservation != null ? SelectedReservation.ReservationTime.Hour + "" : "0"; set { } }
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
                if (SelectedReservation != null)
                {
                    int.TryParse(value, out int no);
                    SelectedReservation.NoOfPeople = no;
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
        public DateTime ReservationTime
        {
            get { return SelectedReservation != null ? SelectedReservation.ReservationTime : DateTime.Now; }
            set
            {
                if (SelectedReservation != null)
                {
                    SelectedReservation.ReservationTime = value;
                }
            }
        }
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
                if (SelectedReservation != null)
                {
                    IRepository<CustomerDTO> ir = new CustomerRepository();
                    int.TryParse(value, out int id);
                    SelectedReservation.Customer = ir.Get(id);
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

        public ManageReservationViewModel()
        {
            InitRelayCommands();
            UpdateAvailableTables();
        }

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

        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged([CallerMemberName] string name = null)
        {
            this.PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
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
            OnPropertyChanged("AvailableTables");
        }
        private void MinusHoursFromReservationTime()
        {
            if (SelectedReservation != null) SelectedReservation.ReservationTime = TrimDateTime(SelectedReservation.ReservationTime.AddHours(-1));
            UpdateAvailableTables();
            OnPropertyChanged("GetReservationTimeDate");
            OnPropertyChanged("GetReservationTimeHours");
        }
        private void AddHoursToReservationTime()
        {
            if (SelectedReservation != null) SelectedReservation.ReservationTime = TrimDateTime(SelectedReservation.ReservationTime.AddHours(1));
            UpdateAvailableTables();

            this.OnPropertyChanged("GetReservationTimeDate");
            this.OnPropertyChanged("GetReservationTimeHours");
        }
        private void AddMinutsToReservationTime()
        {
            if (SelectedReservation != null) SelectedReservation.ReservationTime = TrimDateTime(SelectedReservation.ReservationTime.AddMinutes(15));
            UpdateAvailableTables();

            this.OnPropertyChanged("GetReservationTimeDate");
            this.OnPropertyChanged("GetReservationTimeMinuts");
            this.OnPropertyChanged("GetReservationTimeHours");

        }
        private void MinMinutsFromReservationTime()
        {
            if (SelectedReservation != null) SelectedReservation.ReservationTime = TrimDateTime(SelectedReservation.ReservationTime.AddMinutes(-15));
            UpdateAvailableTables();
            this.OnPropertyChanged("GetReservationTimeDate");
            this.OnPropertyChanged("GetReservationTimeMinuts");
            this.OnPropertyChanged("GetReservationTimeHours");
        }
        private DateTime TrimDateTime(DateTime dt)
        {
            if (dt.Minute > 0 && dt.Minute < 15)
            {
                while (dt.Minute < 15)
                {
                    dt = dt.AddMinutes(1);
                }
            }
            else if (dt.Minute > 15 && dt.Minute < 30)
            {
                while (dt.Minute < 30)
                {
                    dt = dt.AddMinutes(1);
                }
            }
            else if (dt.Minute > 30 && dt.Minute < 45)
            {
                while (dt.Minute < 45)
                {
                    dt = dt.AddMinutes(1);
                }
            }
            else if (dt.Minute > 45 && dt.Minute <= 59)
            {
                while (dt.Minute > 0)
                {
                    dt = dt.AddMinutes(-1);
                }
                SelectedReservation.ReservationTime = SelectedReservation.ReservationTime.AddHours(1);
                OnPropertyChanged("GetReservationTimeHours");
            }
            return dt;
        }
        public void UpdateReservation()
        {

        }
        public void RemoveReservation()
        {

        }
        public void OrderFood()
        {
            if (SelectedReservation.Id > 0)
            {
                MainWindow.ChangeFrame(new OrderFood(SelectedReservation.Id));
            }
            else
            {
                ReservationDTO _reservation = CreateReservation();
                if (_reservation == null) return;
                UpdateSelectedReservation(_reservation);
                MainWindow.ChangeFrame(new OrderFood(_reservation.Id));
            }
        }
        private void UpdateSelectedReservation(ReservationDTO reservation)
        {
            _selectedReservation = reservation;
            OnPropertyChanged("ReservationComment");
            OnPropertyChanged("ReservationNumber");
            OnPropertyChanged("ReservationTables");
            OnPropertyChanged("ReservationNumOfPersons");
            OnPropertyChanged("ReservationDate");
            OnPropertyChanged("ReservationTime");
            OnPropertyChanged("GetReservationTimeDate");
            OnPropertyChanged("ReservationDeposit");
            OnPropertyChanged("ReservationCustomer");
            OnPropertyChanged("GetReservationTimeMinuts");
            OnPropertyChanged("GetReservationTimeHours");
        }
        public void CreateAndExitReservation()
        {
            CreateReservation();
        }
        public ReservationDTO CreateReservation()
        {
            SelectedReservation.Tables = ReservationTables.ToList();
            var res = _reservationRepository.Create(SelectedReservation);
            if (res == null) MessageBox.Show("Fejl ved oprettelse af reservation");
            return res;
        }
        private void ClearValues()
        {
            UpdateSelectedReservation(new ReservationDTO()
            {
                ReservationDate = DateTime.Now,
                Deposit = false,
            });
        }
        #endregion
    }
}
