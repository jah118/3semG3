using DataAccess.DataTransferObjects;
using GalaSoft.MvvmLight.Command;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RestaurantDesktopClient.Views.ViewModels
{
    class OrderFoodModelView : INotifyPropertyChanged
    {
        public RelayCommand AddFoodCommand { get; set; }
        public RelayCommand AddDrinkCommand { get; set; }
        public RelayCommand RemoveSummary { get; set; }

        private FoodDTO _selectedFood;
        private FoodDTO _selectedDrink;
        private FoodDTO _selectedSummary;
        public FoodDTO SelectedFood
        {
            get { return _selectedFood; }
            set
            {
                _selectedFood = value;
                OnPropertyChanged("SelectedFood");
                AddFoodCommand.RaiseCanExecuteChanged();
            }
        }
        public FoodDTO SelectedDrink
        {
            get { return _selectedDrink; }
            set
            {
                _selectedDrink = value;
                OnPropertyChanged("SelectedDrink");
                AddDrinkCommand.RaiseCanExecuteChanged();
            }
        }
        public FoodDTO SelectedSummaryFood
        {
            get { return _selectedSummary; }
            set
            {
                _selectedSummary = value;
                OnPropertyChanged("SelectedSummary");
                RemoveSummary.RaiseCanExecuteChanged();
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;
        private void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
        public ObservableCollection<FoodDTO> Summary { get; set; }
        public OrderFoodModelView(SummaryFoodsViewModel summaryFoodsViewModel, SearchFoodsViewModel searchFoodsViewModel, SearchDriksViewModel searchDriksViewModel)
        {
            Summary = new ObservableCollection<FoodDTO>();
            AddFoodCommand = new RelayCommand(AddFoodToSummary);
            AddDrinkCommand = new RelayCommand(AddDringToSummary);
            RemoveSummary = new RelayCommand(RemoveFromSummary);//, () => { return SelectedSummaryFood != null; });
        }
        private void AddDringToSummary()
        {

        }
        private void AddFoodToSummary()
        {

        }
        private void RemoveFromSummary()
        {

        }
    }
}
