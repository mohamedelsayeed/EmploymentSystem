using EmploymentSystem.Application.Services;
using EmploymentSystem.Domain.DTOs;
using Microsoft.AspNetCore.Mvc;

namespace EmploymentSystem.WebApi.Controllers
{
    public class ApplicationsController : BaseApiController
    {
        private readonly IApplicationServices _applicationServices;

        public ApplicationsController(IApplicationServices applicationServices)
        {
            _applicationServices = applicationServices;
        }

        [HttpPost("Apply")]
        public async Task<ActionResult<bool>> CreateApplication([FromBody]ApplicationDto applicationDto)
        {
            var result = await _applicationServices.CreateApplication(applicationDto);
            return Ok(applicationDto);
        }
    }
}
