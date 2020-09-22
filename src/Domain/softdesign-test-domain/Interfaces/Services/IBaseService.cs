using System.Collections.Generic;
using System.Threading.Tasks;

namespace softdesign_test_domain.Interfaces.Services
{
    public interface IBaseService<TEntity>
    {
        List<TEntity> Get();
        Task<TEntity> Get(string id);
        Task DeleteAsync(string id);
        Task InsertAsync(TEntity entity);
        Task UpdateAsync(string id, TEntity entity);
    }
}