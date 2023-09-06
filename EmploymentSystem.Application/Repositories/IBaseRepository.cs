using EmploymentSystem.Domain.Common;
using System.Linq.Expressions;

namespace EmploymentSystem.Application.Repositories;

public interface IBaseRepository<T> where T : BaseEntity
{
    Task CreateAsync(T entity);
    void Update(T entity);
    void Delete(T entity);
    Task<T> Get(int id);
    Task<List<T>> GetAll(Expression<Func<T, bool>> filter = null);
}
