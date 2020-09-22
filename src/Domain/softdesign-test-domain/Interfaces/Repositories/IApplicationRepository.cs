using softdesign_test_domain.Models.Entity;

namespace softdesign_test_domain.Interfaces.Repositories
{
    public interface IApplicationRepository : IBaseMongoRepository<ApplicationEntity>
    {
    }
}
