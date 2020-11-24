using DataAccess.DataTransferObjects;
using System.Collections.Generic;

namespace RestaurantDesktopClient.Views.ViewModels
{
    internal interface IFoodRepository
    {
        List<FoodDTO> GetAllFoods();
    }
}