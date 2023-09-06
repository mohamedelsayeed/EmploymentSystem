using EmploymentSystem.Application.Repositories;
using EmploymentSystem.Persistance.Context;

namespace EmploymentSystem.Persistance.Repositories;

public class ApplicationRepository : BaseRepository<Domain.Entities.Application>, IApplicationRepository
{
    public ApplicationRepository(AppIdentityDBContext context) : base(context)
    {
    }
}
