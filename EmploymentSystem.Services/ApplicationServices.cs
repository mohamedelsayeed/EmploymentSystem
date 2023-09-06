using EmploymentSystem.Application.Repositories;
using EmploymentSystem.Application.Services;
using EmploymentSystem.Domain.DTOs;

namespace EmploymentSystem.Services;

public class ApplicationServices : IApplicationServices
{
    private readonly IApplicationRepository _applicationRepository;
    private readonly IUnitOfWork _unitOfWork;
    private readonly IVacancyRepository _vacancyRepository;
    public ApplicationServices(IUnitOfWork unitOfWork,
                               IApplicationRepository applicationRepository,
                               IVacancyRepository vacancyRepository)
    {
        _unitOfWork = unitOfWork;
        _applicationRepository = applicationRepository;
        _vacancyRepository = vacancyRepository;
    }

    public async Task<Domain.Entities.Application> CreateApplication(ApplicationDto applicationDto)
    {
        var vacancy = await _vacancyRepository.Get(applicationDto.VacancyId);
        if (vacancy.CurrentApplications == vacancy.MaxApplications)
            return null;//should return cant create application with reason
        
        var applicantApplications = await _applicationRepository.GetAll(a =>
            a.ApplicantId == applicationDto.ApplicantId
            && (a.ApplicationDate >= DateTime.UtcNow.AddDays(-1)));
        
        if(applicantApplications.Any())
            return null;//should return cant create application with reason

        var application = new Domain.Entities.Application
        {
            ApplicantId = applicationDto.ApplicantId,
            VacancyId = applicationDto.VacancyId,
            ApplicationDate = DateTime.UtcNow
        };

        await _applicationRepository.CreateAsync(application);

        vacancy.CurrentApplications += 1;

        var result = await _unitOfWork.SaveAsync();

        if (result > 0)
            return application;

        return null;
    }

    public async Task<IEnumerable<ApplicationDto>> GetAllApplicationByVacancyId(int vacancyId)
    {

        var applications = await _applicationRepository.GetAll(a => a.VacancyId == vacancyId);

        return applications.Select(a => new ApplicationDto
        {
            ApplicantId = a.ApplicantId,
            ApplicationDate = a.ApplicationDate,
            VacancyId = a.VacancyId
        }).ToList();
    }

    public Task<ApplicationDto> GetApplication(int id)
    {
        throw new NotImplementedException();
    }

    public Task<Domain.Entities.Application> UpdateApplication(Domain.Entities.Application Application)
    {
        throw new NotImplementedException();
    }
}