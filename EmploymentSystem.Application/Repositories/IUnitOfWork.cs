using EmploymentSystem.Domain.Common;

namespace EmploymentSystem.Application.Repositories;

public interface IUnitOfWork : IAsyncDisposable
{
    IBaseRepository<TEntity> Repository<TEntity>() where TEntity : BaseEntity;

    Task<int> SaveAsync();
}
