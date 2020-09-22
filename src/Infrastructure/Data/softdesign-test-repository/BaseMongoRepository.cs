using MongoDB.Bson;
using MongoDB.Driver;
using softdesign_test_domain.Interfaces.Repositories;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace softdesign_test_repository
{
    public class BaseMongoRepository<TEntity> : IBaseMongoRepository<TEntity>
    {
        protected readonly string _entityName;
        protected readonly IMongoDatabase _mongoDatabase;

        public BaseMongoRepository(IMongoDatabase mongoDatabase)
        {
            _entityName = typeof(TEntity).Name;
            _mongoDatabase = mongoDatabase;
        }

        public List<TEntity> Get()
        {
            return _mongoDatabase.GetCollection<TEntity>(_entityName).AsQueryable().ToList();
        }

        public async Task<TEntity> Get(string id)
        {
            var filter = Builders<TEntity>.Filter.Eq("_id", ObjectId.Parse(id));

            var response = await _mongoDatabase.GetCollection<TEntity>(_entityName).FindAsync(filter);
            return response.FirstOrDefault();
        }

        public async Task InsertAsync(TEntity entity)
        {
            await _mongoDatabase.GetCollection<TEntity>(_entityName).InsertOneAsync(entity);
        }

        public async Task UpdateAsync(string id, TEntity entity)
        {
            var filter = Builders<TEntity>.Filter.Eq("_id", ObjectId.Parse(id));

            await _mongoDatabase.GetCollection<TEntity>(_entityName).ReplaceOneAsync(filter, entity);
        }

        public async Task DeleteAsync(string id)
        {
            var filter = Builders<TEntity>.Filter.Eq("_id", ObjectId.Parse(id));

            await _mongoDatabase.GetCollection<TEntity>(_entityName).DeleteOneAsync(filter);
        }
    }
}
