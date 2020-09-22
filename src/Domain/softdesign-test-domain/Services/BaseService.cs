using softdesign_test_domain.Interfaces.Repositories;
using softdesign_test_domain.Interfaces.Services;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace softdesign_test_domain.Services
{
    public class BaseService<TEntity> : IBaseService<TEntity>
    {
        protected IBaseMongoRepository<TEntity> _repository { get; set; }

        public BaseService(IBaseMongoRepository<TEntity> repository)
        {
            _repository = repository;
        }

        public async Task DeleteAsync(string id)
        {
            await _repository.DeleteAsync(id);
        }

        public List<TEntity> Get()
        {
            return _repository.Get();
        }

        public async Task<TEntity> Get(string id)
        {
            return await _repository.Get(id);
        }

        public async Task InsertAsync(TEntity entity)
        {
            await _repository.InsertAsync(entity);
        }

        public async Task UpdateAsync(string id, TEntity entity)
        {
            await _repository.UpdateAsync(id, entity);
        }
    }
}
