using MongoDB.Driver;
using softdesign_test_domain.Interfaces.Repositories;
using softdesign_test_domain.Models.Entity;

namespace softdesign_test_repository
{
    public class ApplicationRepository : BaseMongoRepository<ApplicationEntity>, IApplicationRepository
    {
        public ApplicationRepository(IMongoDatabase mongoDatabase) : base(mongoDatabase) { }
    }
}
