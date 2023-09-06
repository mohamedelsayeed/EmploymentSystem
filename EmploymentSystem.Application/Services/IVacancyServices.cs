using EmploymentSystem.Domain.DTOs;
using EmploymentSystem.Domain.Entities;
using System.Linq.Expressions;

namespace EmploymentSystem.Application.Services;

public interface IVacancyServices
{
    //create
    Task<Vacancy> CreateVacancy(VacancyDto vacancy);

    //update
    Task<Vacancy> UpdateVacancy(Vacancy vacancy);

    //delete
    Task<bool> DeleteVacancy(int id);

    //getbyid
    Task<VacancyDto> GetVacancy(int id);

    //getallavaible
    Task<IEnumerable<VacancyDto>> GetAllVacancy(Expression<Func<Vacancy, bool>> filter = null);

}
