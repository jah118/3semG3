using System.Collections.Generic;
using System.Net;
using RestSharp;

namespace RestaurantDesktopClient.Services
{
    public interface IRepository<T>
    {
        IEnumerable<T> GetAll();
        T Get(int id);
        T Create(T t);
        T Update(T t);
        HttpStatusCode Delete(T t);
    }
}