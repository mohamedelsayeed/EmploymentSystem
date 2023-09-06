using EmploymentSystem.Application.Repositories;
using EmploymentSystem.Domain.Common;
using EmploymentSystem.Persistance.Context;
using EmploymentSystem.Persistance.Repositories;
using System.Collections;

namespace EmploymentSystem.Persistance;

public class UnitOfWork : IUnitOfWork
{
    private readonly AppIdentityDBContext _appIdentityDBContext;
    private Hashtable _repositories;

    public UnitOfWork(AppIdentityDBContext appIdentityDBContext)
    {
        _appIdentityDBContext = appIdentityDBContext;
    }

    public IBaseRepository<TEntity> Repository<TEntity>() where TEntity : BaseEntity
    {
        if (_repositories == null)
            _repositories = new Hashtable();
        var type = typeof(TEntity).Name;
        if (!_repositories.ContainsKey(type))
        {
            var repository = new BaseRepository<TEntity>(_appIdentityDBContext);
            _repositories.Add(type, repository);
        }
        return _repositories[type] as IBaseRepository<TEntity>;
    }

    public async Task<int> SaveAsync()
        => await _appIdentityDBContext.SaveChangesAsync();

    public async ValueTask DisposeAsync()
        => await _appIdentityDBContext.DisposeAsync();

}
