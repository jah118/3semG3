using System.Collections.Generic;

namespace RestaurantClientService.Services
{
    public interface IRepository<T>
    {
        IEnumerable<T> GetAll();
        T Get(int id);
        T Create(T t);
    }
}