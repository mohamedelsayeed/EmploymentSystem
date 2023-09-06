using EmploymentSystem.Application.Repositories;
using EmploymentSystem.Domain.Entities;
using EmploymentSystem.Persistance.Context;

namespace EmploymentSystem.Persistance.Repositories;

public class VacancyRepository : BaseRepository<Vacancy>, IVacancyRepository
{
    public VacancyRepository(AppIdentityDBContext context) : base(context)
    {
    }
}
