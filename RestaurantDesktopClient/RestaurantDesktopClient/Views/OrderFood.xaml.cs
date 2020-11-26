using RestaurantDesktopClient.Services.OrderService;
using RestaurantDesktopClient.Views.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace RestaurantDesktopClient.Views
{
    /// <summary>
    /// Interaction logic for OrderFood.xaml
    /// </summary>
    public partial class OrderFood : Page
    {
        public OrderFood(int id)
        {
            InitializeComponent();
            SummaryFoodsViewModel summaryFoodsViewModel = new SummaryFoodsViewModel(id);
            SearchFoodsViewModel searchFoodsViewModel = new SearchFoodsViewModel();
            SearchDriksViewModel searchDriksViewModel = new SearchDriksViewModel();
            OrderFoodModelView OrderFoodModelView = new OrderFoodModelView(summaryFoodsViewModel, searchFoodsViewModel, searchDriksViewModel);


            SummaryListPage.DataContext = summaryFoodsViewModel;
            FoodListPage.DataContext = searchFoodsViewModel;
            DrinkListPage.DataContext = searchDriksViewModel;

            //Binding bindingSummary = new Binding() { Mode = BindingMode.TwoWay, Source = OrderFoodModelView.SelectedSummaryFood, Path = new PropertyPath(".") };
            //BindingOperations.SetBinding(SummaryListPage.dgResult, DataGrid.SelectedItemProperty, bindingSummary);

            //Binding bindingDrink = new Binding() { Mode = BindingMode.TwoWay, Source = OrderFoodModelView.SelectedDrink, Path = new PropertyPath(".") };
            //BindingOperations.SetBinding(DrinkListPage.dgResult, DataGrid.SelectedItemProperty, bindingDrink);

            //Binding bindingFood = new Binding() { Mode = BindingMode.TwoWay, Source = OrderFoodModelView.SelectedFood, Path = new PropertyPath(".") };
            //BindingOperations.SetBinding(FoodListPage.dgResult, DataGrid.SelectedItemProperty, bindingFood);
        }

    }
}
