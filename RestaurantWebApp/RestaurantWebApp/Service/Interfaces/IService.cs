using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;
using RestSharp;

namespace RestaurantWebApp.Service.Interfaces
{
    public interface IService<TEntity>
    {
        IEnumerable<TEntity> GetAll();

        TEntity GetById(int id);

        bool Create(TEntity obj);

        TEntity Update(TEntity obj);

        bool Delete(TEntity obj);
        Task<IEnumerable<TEntity>> GetAllAsync();

        Task<TEntity> GetByIdAsync(int id);

        Task<HttpStatusCode> CreateAsync(TEntity obj);

        Task<TEntity> UpdateAsync(TEntity obj);

        Task<bool> DeleteAsync(TEntity obj);
    }
}