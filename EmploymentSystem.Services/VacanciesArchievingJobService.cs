using EmploymentSystem.Application.Repositories;
using Quartz;

namespace EmploymentSystem.Services;

public class VacanciesArchievingJobService : IJob
{
    
    private readonly IVacancyRepository _vacancyRepository;
    private readonly IUnitOfWork _unitOfWork;
    public VacanciesArchievingJobService(IVacancyRepository vacancyRepository, IUnitOfWork unitOfWork)
    {
        _vacancyRepository = vacancyRepository;
        _unitOfWork = unitOfWork;
    }

    public async Task Execute(IJobExecutionContext context)
    {
        var vacancies = await _vacancyRepository.GetAll(a => a.ExpiryDate >= DateTime.UtcNow);
        
        vacancies.ForEach(vacancy => { vacancy.IsArchived = true; });
        await _unitOfWork.SaveAsync();
        Console.WriteLine("Job Excuting");
    }
}
