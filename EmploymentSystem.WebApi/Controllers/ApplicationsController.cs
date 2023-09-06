using EmploymentSystem.Application.Services;
using EmploymentSystem.Domain.DTOs;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace EmploymentSystem.WebApi.Controllers;

public class ApplicationsController : BaseApiController
{
    private readonly IApplicationServices _applicationServices;

    public ApplicationsController(IApplicationServices applicationServices)
    {
        _applicationServices = applicationServices;
    }
    [Authorize(Roles = "Applicant")]
    [HttpPost("Apply")]
    public async Task<ActionResult<bool>> CreateApplication([FromBody]ApplicationDto applicationDto)
    {
        var result = await _applicationServices.CreateApplication(applicationDto);
        return Ok(result);
    }

    [Authorize(Roles = "Employer")]
    [HttpGet("GetApplicationsByVacancyId")]
    public async Task<ActionResult<List<ApplicationDto>>> GetApplicationsByVacancyId(int VacancyId)
    {
        var result = await _applicationServices.GetAllApplicationByVacancyId(VacancyId);
        return Ok(result);
    }

}
