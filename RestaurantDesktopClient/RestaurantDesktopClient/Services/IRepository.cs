using DataAccess.DataTransferObjects;
using System.Collections.Generic;

namespace RestaurantDesktopClient.Views.ViewModels
{
    internal interface IRepository<T>
    {
        IEnumerable<T> GetAll();
        T Get(int id);
        T Create(T t);
    }
}