using DataAccess.DataTransferObjects;
using System.Collections.Generic;

namespace RestaurantDesktopClient.Services
{
    public interface IRepository<T>
    {
        IEnumerable<T> GetAll();
        T Get(int id);
        T Create(T t);
    }
}