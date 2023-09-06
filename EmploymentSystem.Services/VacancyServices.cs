using EmploymentSystem.Application.Repositories;
using EmploymentSystem.Application.Services;
using EmploymentSystem.Domain.DTOs;
using EmploymentSystem.Domain.Entities;
using System.Data;
using System.Linq.Expressions;

namespace EmploymentSystem.Services;

public class VacancyServices : IVacancyServices
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IVacancyRepository _vacancyRepository;
    public VacancyServices(IUnitOfWork unitOfWork, IVacancyRepository vacancyRepository)
    {
        _unitOfWork = unitOfWork;
        _vacancyRepository = vacancyRepository;
    }
    public async Task<Vacancy> CreateVacancy(VacancyDto vacancyDto)
    {
        var vacancey = new Vacancy
        {
            CurrentApplications = vacancyDto.CurrentApplications,
            Description = vacancyDto.Description,
            EmployerId = vacancyDto.EmployerId,
            ExpiryDate = vacancyDto.ExpiryDate,
            IsClosed = vacancyDto.IsClosed,
            Title = vacancyDto.Title,
            MaxApplications = vacancyDto.MaxApplications,
        };
        await _vacancyRepository.CreateAsync(vacancey);

        var result = await _unitOfWork.SaveAsync();

        if (result > 0)
            return vacancey;

        return null;
    }

    public async Task<bool> DeleteVacancy(int id)
    {
        var vacancy = await _vacancyRepository.Get(id);
        vacancy.IsDeleted = true;
        _vacancyRepository.Delete(vacancy);
        var result = await _unitOfWork.SaveAsync();
        return result > 0;
    }
    public async Task<bool> DeactivateVacancy(int id)
    {
        var vacancy = await _vacancyRepository.Get(id);
        vacancy.IsClosed = true;
        var result = await _unitOfWork.SaveAsync();
        return result > 0;
    }
    public async Task<bool> ActivateVacancy(int id)
    {
        var vacancy = await _vacancyRepository.Get(id);
        vacancy.IsClosed = false;
        var result = await _unitOfWork.SaveAsync();
        return result > 0;
    }

    public async Task<IEnumerable<VacancyDto>> GetAllVacancy(Expression<Func<Vacancy, bool>> filter = null)
    {
        var vaccancies = await _vacancyRepository.GetAll(filter);

        return vaccancies.Select(a => new VacancyDto
        {
            CurrentApplications = a.CurrentApplications,
            Description = a.Description,
            ExpiryDate = a.ExpiryDate,
            EmployerId = a.EmployerId,
            IsClosed = a.IsClosed,
            MaxApplications = a.MaxApplications,
            Title = a.Title
        }).ToList();
    }

    public async Task<VacancyDto> GetVacancy(int id)
    {
        var vacancy = await _vacancyRepository.Get(id);
        return new VacancyDto
        {
            CurrentApplications = vacancy.CurrentApplications,
            Description = vacancy.Description,
            ExpiryDate = vacancy.ExpiryDate,
            EmployerId = vacancy.EmployerId,
            IsClosed = vacancy.IsClosed,
            MaxApplications = vacancy.MaxApplications,
            Title = vacancy.Title,
            Id = vacancy.Id
        };
    }

    public async Task<Vacancy> UpdateVacancy(VacancyDto vacancy)
    {
        var vacancyResult = await _vacancyRepository.Get(vacancy.Id.Value);

        vacancyResult.Description = vacancy.Description;
        vacancyResult.ExpiryDate = vacancy.ExpiryDate;
        vacancyResult.EmployerId = vacancy.EmployerId;
        vacancyResult.Title = vacancy.Title;
        vacancyResult.MaxApplications = vacancy.MaxApplications;


        _vacancyRepository.Update(vacancyResult);
        await _unitOfWork.SaveAsync();
        return vacancyResult;
    }

}