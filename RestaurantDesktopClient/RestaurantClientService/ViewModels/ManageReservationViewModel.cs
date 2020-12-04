using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using MvvmCross;
using MvvmCross.Commands;
using MvvmCross.Navigation;
using MvvmCross.ViewModels;
using RestaurantClientService.DataTransferObjects;
using RestaurantClientService.Interaction;
using RestaurantClientService.Services;
using RestaurantClientService.Services.CustomerService;
using RestaurantClientService.Services.ReservationService;
using RestaurantClientService.Services.Table_Service;

namespace RestaurantClientService.ViewModels
{
    public class ManageReservationViewModel : MvxViewModel
    {
        #region Fields
        private static ReservationDTO _selectedReservation;
        private readonly IRepository<ReservationDTO> _reservationRepository;
        private readonly IRepository<TablesDTO> _tableRepository;
        private readonly IRepository<CustomerDTO> _customerRepository;
        private IMvxNavigationService _navigation;
        private MvxInteraction<IBoolQuestion> _boolDialog;
        #endregion
        #region Properties
        public string Headline { get { return "Reservationer"; } }
        public TablesDTO SelectedTables { get; set; }
        public ReservationDTO SelectedReservation
        {
            get { return _selectedReservation ?? (_selectedReservation = new ReservationDTO()); }
            set { UpdateSelectedReservation(value); _selectedReservation = value; }
        }
        public DateTime GetReservationTimeDate
        {
            get => SelectedReservation != null ? SelectedReservation.ReservationTime : DateTime.Now;
            set { if (SelectedReservation != null) { SelectedReservation.ReservationTime = value; }; }
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
                    if (SelectedReservation.Tables == null)
                    {
                        SelectedReservation.Tables = new List<TablesDTO>();
                    }
                    var found = SelectedReservation.Tables.Find(x => x.Id == value.Id);
                    if (found != null)
                    {
                    }
                    else
                    {
                        SelectedReservation.Tables.Add(value);
                    }
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
        public List<TablesDTO> ReservationTables
        {
            get
            {
                if (SelectedReservation.Tables != null)
                {
                    return SelectedReservation.Tables;
                }
                else
                {
                    return _tableRepository.GetAll().ToList();

                }
            }
            set
            {
                if (SelectedReservation != null)
                {
                    SelectedReservation.Tables = value;
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
                    int.TryParse(value, out int id);
                    SelectedReservation.Customer = _customerRepository.Get(id);
                }
            }
        }
       
        #endregion
        #region relayCommands
        public IMvxCommand CreateReservationCommand { get; set; }
        public IMvxCommand RemoveReservationCommand { get; set; }
        public IMvxCommand UpdateReservationCommand { get; set; }
        public IMvxCommand OrderFoodCommand { get; set; }
        public IMvxCommand ReservationTimeAddHours { get; set; }
        public IMvxCommand ReservationTimeMinHours { get; set; }
        public IMvxCommand ReservationTimeAddMinutes { get; set; }
        public IMvxCommand ReservationTimeMinMinutes { get; set; }
        public IMvxCommand ClearValuesCommand { get; set; }
        #endregion

        #region Interactions
        public IMvxInteraction<BoolQuestion> BoolDialog => _boolDialog;
        #endregion

        public ManageReservationViewModel
            (IRepository<ReservationDTO> reservationRepository,
            IRepository<TablesDTO> tableRepository,
            IRepository<CustomerDTO> customerRepository,
            IMvxNavigationService navigation)
        {
            _boolDialog = new MvxInteraction<BoolQuestion>();
            _navigation = navigation;
            _reservationRepository = reservationRepository;
            _tableRepository = tableRepository;
            _customerRepository = customerRepository;
            SelectedTables = new TablesDTO();
            InitCommands();
            
        }

        private void InitCommands()
        {
            ReservationTimeAddHours = new MvxCommand(AddHoursFromReservationTime);
            ReservationTimeMinHours = new MvxCommand(AddHoursToReservationTime);
            ReservationTimeAddMinutes = new MvxCommand(AddMinutesToReservationTime);
            ReservationTimeMinMinutes = new MvxCommand(MinMinutesFromReservationTime);
            CreateReservationCommand = new MvxCommand(CreateAndExitReservation);
            RemoveReservationCommand = new MvxCommand(RemoveReservation);
            UpdateReservationCommand = new MvxCommand(UpdateReservation);
            OrderFoodCommand = new MvxCommand(OrderFood);
            ClearValuesCommand = new MvxCommand(ClearValues);
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
        private void AddHoursToReservationTime()
        {
            if (SelectedReservation != null) SelectedReservation.ReservationTime = TrimDateTime(SelectedReservation.ReservationTime.AddHours(-1));

            this.OnPropertyChanged("GetReservationTimeDate");
            this.OnPropertyChanged("GetReservationTimeHours");
        }
        private void AddHoursFromReservationTime()
        {
            if (SelectedReservation != null) SelectedReservation.ReservationTime = TrimDateTime(SelectedReservation.ReservationTime.AddHours(1));

            this.OnPropertyChanged("GetReservationTimeDate");
            this.OnPropertyChanged("GetReservationTimeHours");
        }
        private void AddMinutesToReservationTime()
        {
            if (SelectedReservation != null) SelectedReservation.ReservationTime = TrimDateTime(SelectedReservation.ReservationTime.AddMinutes(15));

            this.OnPropertyChanged("GetReservationTimeDate");
            this.OnPropertyChanged("GetReservationTimeMinuts");
            this.OnPropertyChanged("GetReservationTimeHours");

        }
        private void MinMinutesFromReservationTime()
        {
            if (SelectedReservation != null) SelectedReservation.ReservationTime = TrimDateTime(SelectedReservation.ReservationTime.AddMinutes(-15));
            this.OnPropertyChanged("GetReservationTimeDate");
            this.OnPropertyChanged("GetReservationTimeMinutes");
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
                _navigation.Navigate<OrderFoodViewModel, ReservationDTO>(SelectedReservation);
            }
            else
            {
                ReservationDTO _reservation = CreateReservation();
                if (_reservation == null) return;
                UpdateSelectedReservation(_reservation);
                _navigation.Navigate<OrderFoodViewModel, ReservationDTO>(SelectedReservation);
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
            OnPropertyChanged("GetReservationTimeMinutes");
            OnPropertyChanged("GetReservationTimeHours");
        }
        public void CreateAndExitReservation()
        {
            CreateReservation();
        }
        public ReservationDTO CreateReservation()
        {
            var res = _reservationRepository.Create(SelectedReservation);
            if (res == null) _boolDialog.Raise(new BoolQuestion()
            {
                BoolCallbackAction = async (ok) =>
                {
                },
                Question = "Fejl ved oprettelse af reservation"
            });//TODO MessageBox.Show("Fejl ved oprettelse af reservation");
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
