using System.Collections.Generic;
using System.Threading.Tasks;

namespace softdesign_test_domain.Interfaces.Repositories
{
    public interface IBaseMongoRepository<TEntity>
    {
        Task DeleteAsync(string id);
        List<TEntity> Get();
        Task<TEntity> Get(string id);
        Task InsertAsync(TEntity entity);
        Task UpdateAsync(string id, TEntity entity);
    }
}