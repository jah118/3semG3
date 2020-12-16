using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;
using RestaurantWebApp.DataTransferObject;
using RestSharp;

namespace RestaurantWebApp.Service.Interfaces
{
    public interface IService<TEntity>
    {
        IEnumerable<TEntity> GetAll();

        TEntity GetById(int id);

        TEntity Create(TEntity obj);

        TEntity Update(TEntity obj);

        bool Delete(TEntity obj);
        Task<IEnumerable<TEntity>> GetAllAsync();

        Task<TEntity> GetByIdAsync(int id);

        Task<IRestResponse> CreateAsync(TEntity obj);

        Task<TEntity> UpdateAsync(TEntity obj);

        Task<bool> DeleteAsync(TEntity obj);
    }
}