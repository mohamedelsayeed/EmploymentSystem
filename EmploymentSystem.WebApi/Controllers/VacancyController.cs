using EmploymentSystem.Application.Services;
using EmploymentSystem.Domain.DTOs;
using EmploymentSystem.Domain.Entities;
using EmploymentSystem.WebApi.Errors;
using Microsoft.AspNetCore.Mvc;

namespace EmploymentSystem.WebApi.Controllers;

public class VacancyController : BaseApiController
{
    private readonly IVacancyServices _vacancyServices;

    public VacancyController(IVacancyServices vacancyServices)
    {
        _vacancyServices = vacancyServices;
    }

    [HttpPost("CreateVacancy")]
    public async Task<ActionResult<Vacancy>> Create([FromBody] VacancyDto vacancyDto)
    {
        if (vacancyDto is null)
            return BadRequest(new ApiValidationErrorResponse() { Errors = new string[] { "model not valid" } });
        var vacancy = await _vacancyServices.CreateVacancy(vacancyDto);
        return Ok(vacancy);
    }

    [HttpGet("GetVacancy")]
    public async Task<ActionResult<VacancyDto>> Get([FromQuery] int id)
    {
        var vacancy = await _vacancyServices.GetVacancy(id);
        if (vacancy is null)
            return NotFound();
        return Ok(vacancy);
    }

    [HttpGet("GetAllVacancy")]
    public async Task<ActionResult<IEnumerable<VacancyDto>>> GetAll(string? title)
    {
        var vacancies = await _vacancyServices.GetAllVacancy(a =>
            (!string.IsNullOrEmpty(title) ? a.Title.Contains(title) : true) 
            && a.IsDeleted != true 
            && a.IsClosed == false
            && a.IsArchived == false
        );
        if (vacancies is null)
            return NotFound();
        return Ok(vacancies);
    }

    [HttpPost("EditVacancy")]
    public async Task<ActionResult<Vacancy>> Update([FromBody] VacancyDto vacancyDto)
    {
        if (vacancyDto is null)
            return BadRequest(new ApiValidationErrorResponse() { Errors = new string[] { "model not valid" } });
        var result = await _vacancyServices.UpdateVacancy(new Vacancy
        {
            CurrentApplications = vacancyDto.CurrentApplications,
            Description = vacancyDto.Description,
            EmployerId = vacancyDto.EmployerId,
            ExpiryDate = vacancyDto.ExpiryDate,
            Id = vacancyDto.Id.Value,
            IsClosed = vacancyDto.IsClosed,
            MaxApplications = vacancyDto.MaxApplications,
            Title = vacancyDto.Title,
        });
        return Ok(result);
    }

    [HttpPost("DeleteVacancy")]
    public async Task<ActionResult<bool>> Delete([FromQuery] int id)
    {
        var result = await _vacancyServices.DeleteVacancy(id);
        return Ok(result);
    }

}
