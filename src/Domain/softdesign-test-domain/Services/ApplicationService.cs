using softdesign_test_domain.Interfaces.Repositories;
using softdesign_test_domain.Interfaces.Services;
using softdesign_test_domain.Models.Entity;

namespace softdesign_test_domain.Services
{
    public class ApplicationService : BaseService<ApplicationEntity>, IApplicationService
    {
        protected readonly IApplicationRepository _applicationRepository;

        public ApplicationService(IApplicationRepository applicationRepository) : base(applicationRepository)
        {
        }
    }
}