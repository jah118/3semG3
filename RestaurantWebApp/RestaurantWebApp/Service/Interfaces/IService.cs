using System.Collections.Generic;

namespace RestaurantWebApp.Service.Interfaces
{
    public interface IService<TEntity>
    {
        IEnumerable<TEntity> GetAll();

        TEntity GetById(int id);

        TEntity Create(TEntity obj);

        TEntity Update(TEntity obj);

        bool Delete(TEntity obj);
    }
}