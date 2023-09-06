using EmploymentSystem.Application.Repositories;
using EmploymentSystem.Domain.Common;
using EmploymentSystem.Persistance.Context;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace EmploymentSystem.Persistance.Repositories;


public class BaseRepository<T> : IBaseRepository<T> where T : BaseEntity
{
    protected readonly AppIdentityDBContext Context;

    public BaseRepository(AppIdentityDBContext context)
    {
        Context = context;
    }

    public async Task CreateAsync(T entity)
    {
       await Context.AddAsync(entity);
    }

    public void Update(T entity)
    {
        Context.Update(entity);
    }

    public void Delete(T entity)
    {
        Context.Update(entity);
    }

    public Task<T> Get(int id)
    {
        return Context.Set<T>().FirstOrDefaultAsync(x => x.Id == id);
    }

    public Task<List<T>> GetAll(Expression<Func<T, bool>> filter = null)
    {
        return Context.Set<T>().Where(filter).ToListAsync();
    }

   
}
