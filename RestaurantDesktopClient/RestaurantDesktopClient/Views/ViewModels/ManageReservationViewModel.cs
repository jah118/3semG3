using DataAccess.DataTransferObjects;
using RestaurantDesktopClient.Reservation;
using RestaurantDesktopClient.Services.CustomerService;
using RestaurantDesktopClient.Services.Table_Service;
using RestaurantDesktopClient.Views.ViewModels;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Data;
using System.Linq;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using System.Windows.Navigation;

namespace RestaurantDesktopClient.Views.ManageReservation
{
    class ManageReservationViewModel : INotifyPropertyChanged
    {
        private DataTable _reservationTable;
        private DataRowView _selectedRowView;
        private static ReservationDTO _selectedReservation;
        public TablesDTO SelectedTables { get; set; }
        public ReservationDTO SelectedReservation { get { return _selectedReservation != null ? _selectedReservation : _selectedReservation = new ReservationDTO(); } set { _selectedReservation = value; } }
        private readonly IRepository<ReservationDTO> repository = new ReservationRepository();
        public RelayCommand CreateReservationCommand { get; set; }
        public RelayCommand RemoveReservationCommand { get; set; }
        public RelayCommand UpdateReservationCommand { get; set; }
        public RelayCommand OrderFoodCommand { get; set; }
        public RelayCommand ReservationTimeAddHours { get; set; }
        public RelayCommand ReservationTimeMinHours { get; set; }
        public RelayCommand ReservationTimeAddMin { get; set; }
        public RelayCommand ReservationTimeMinMinuts { get; set; }
        public DateTime GetReservationTimeDate
        {
            get => SelectedReservation != null ? SelectedReservation.ReservationTime : DateTime.Now;
            set { if (SelectedReservation != null) { SelectedReservation.ReservationTime = value; }; }
        }
        public string GetReservationTimeMinuts { get => SelectedReservation != null ? trimDateTime(SelectedReservation.ReservationTime).Minute + "" : "0"; set { } }
        public string GetReservationTimeHours { get => SelectedReservation != null ? SelectedReservation.ReservationTime.Hour + "" : "0"; set { } }
        public string Headline { get { return "Reservationer"; } }
        public ManageReservationViewModel()
        {
            ReservationTimeAddHours = new RelayCommand(AddHoursFromReservationTime);
            ReservationTimeMinHours = new RelayCommand(AddHoursToReservationTime);
            ReservationTimeAddMin = new RelayCommand(AddMinutsToReservationTime);
            ReservationTimeMinMinuts = new RelayCommand(MinMinutsFromReservationTime);
            CreateReservationCommand = new RelayCommand(CreateAndExitReservation);
            RemoveReservationCommand = new RelayCommand(RemoveReservation);
            UpdateReservationCommand = new RelayCommand(UpdateReservation);
            OrderFoodCommand = new RelayCommand(OrderFood);
            SelectedTables = new TablesDTO();
        }
        public TablesDTO SetSelectedTables
        {
            get
            {
                return SelectedReservation.Tables != null ? SelectedReservation.Tables.FirstOrDefault() ?? null : new TablesDTO();
            }
            set
            {
                if (SelectedReservation.Tables == null)
                {
                    SelectedReservation.Tables = new List<TablesDTO>();
                }
                    var found = SelectedReservation.Tables.Find(x => x.TableNumber == value.TableNumber);
                if(found != null)
                {
                }
                else
                {
                    SelectedReservation.Tables.Add(value);
                }
            }
        }
        private void AddHoursToReservationTime()
        {
            if (SelectedReservation != null) SelectedReservation.ReservationTime = trimDateTime(SelectedReservation.ReservationTime.AddHours(-1));

            this.OnPropertyChanged("GetReservationTimeDate");
            this.OnPropertyChanged("GetReservationTimeHours");
        }
        private void AddHoursFromReservationTime()
        {
            if (SelectedReservation != null) SelectedReservation.ReservationTime = trimDateTime(SelectedReservation.ReservationTime.AddHours(1));

            this.OnPropertyChanged("GetReservationTimeDate");
            this.OnPropertyChanged("GetReservationTimeHours");
        }
        private void AddMinutsToReservationTime()
        {
            if (SelectedReservation != null) SelectedReservation.ReservationTime = trimDateTime(SelectedReservation.ReservationTime.AddMinutes(15));

            this.OnPropertyChanged("GetReservationTimeDate");
            this.OnPropertyChanged("GetReservationTimeMinuts");
            this.OnPropertyChanged("GetReservationTimeHours");

        }
        private void MinMinutsFromReservationTime()
        {
            if (SelectedReservation != null) SelectedReservation.ReservationTime = trimDateTime(SelectedReservation.ReservationTime.AddMinutes(-15));
            this.OnPropertyChanged("GetReservationTimeDate");
            this.OnPropertyChanged("GetReservationTimeMinuts");
            this.OnPropertyChanged("GetReservationTimeHours");
        }
        private DateTime trimDateTime(DateTime dt)
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
            }
            return dt;
        }
        public List<string> Tables()
        {
            IRepository<TablesDTO> repos = new TableRepository();
            var temp = repos.GetAll();
            var res = from TablesDTO in temp select TablesDTO.Id + "";
            return res.ToList();
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
                var res = new List<TablesDTO>();
                if (SelectedReservation.Tables != null)
                {
                    res = SelectedReservation.Tables;
                }
                else
                {
                    IRepository<TablesDTO> ir = new TableRepository();
                    res = ir.GetAll().ToList();

                }

                return res;
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
            get { return SelectedReservation != null ? SelectedReservation.Deposit : false; }
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
        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged([CallerMemberName] string name = null)
        {
            this.PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
        }
        private void CreateSearchTable()
        {
            _reservationTable = new DataTable();
            _reservationTable.Columns.Add(new DataColumn("Reservation Number"));
            _reservationTable.Columns.Add(new DataColumn("Customer"));
            _reservationTable.Columns.Add(new DataColumn("ReservationDate"));
            _reservationTable.Columns.Add(new DataColumn("ReservationTime"));
            _reservationTable.Columns.Add(new DataColumn("NoOfPeople"));
        }
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
                UpdateSelectedReservation(repository.Get(id));

            }
        }
        public DataTable SearchTable
        {

            get
            {
                IEnumerable<ReservationDTO> _reservations = repository.GetAll();
                if (_reservations != null)
                {

                    if (_reservationTable == null) CreateSearchTable();

                    _reservations.ToList().ForEach(x =>
                    {
                        DataRow dr = _reservationTable.NewRow();
                        _reservationTable.Rows.Add(dr.ItemArray = new[] { x.Id+"", x.Customer.FirstName + " " + x.Customer.LastName,
                            x.ReservationDate+"", x.ReservationTime+"", x.NoOfPeople +"" });
                    });
                }
                return _reservationTable;
            }
            set
            {
                _reservationTable = value;
                PropertyChanged(this, new PropertyChangedEventArgs("SearchTable"));
            }
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
                UpdateSelectedReservation(_reservation);
                MainWindow.ChangeFrame(new OrderFood(_reservation.Id));
            }
        }
        private void UpdateSelectedReservation(ReservationDTO reservation)
        {
            SelectedReservation = reservation;
            this.OnPropertyChanged("ReservationComment");
            this.OnPropertyChanged("ReservationNumber");
            this.OnPropertyChanged("ReservationTables");
            this.OnPropertyChanged("ReservationNumOfPersons");
            this.OnPropertyChanged("ReservationDate");
            this.OnPropertyChanged("ReservationTime");
            this.OnPropertyChanged("GetReservationTimeDate");
            this.OnPropertyChanged("ReservationDeposit");
            this.OnPropertyChanged("ReservationCustomer");
            this.OnPropertyChanged("GetReservationTimeMinuts");
            this.OnPropertyChanged("GetReservationTimeHours");
        }
        public void CreateAndExitReservation()
        {
            CreateReservation();
        }
        public ReservationDTO CreateReservation()
        {
            var res = repository.Create(SelectedReservation);
            return res;
        }
    }
}
