using EmploymentSystem.Domain.DTOs;

namespace EmploymentSystem.Application.Services;

public interface IApplicationServices
{
    //create
    Task<Domain.Entities.Application> CreateApplication(ApplicationDto Application);

    //update
    Task<Domain.Entities.Application> UpdateApplication(Domain.Entities.Application Application);

    //getbyid
    Task<ApplicationDto> GetApplication(int id);

    //getallavaible
    Task<IEnumerable<ApplicationDto>> GetAllApplicationByVacancyId(int vacancyId);
}
