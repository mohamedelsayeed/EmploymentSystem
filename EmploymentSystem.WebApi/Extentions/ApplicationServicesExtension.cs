using EmploymentSystem.Application.Authentication;
using EmploymentSystem.Application.Repositories;
using EmploymentSystem.Application.Services;
using EmploymentSystem.Persistance;
using EmploymentSystem.Persistance.Authentication;
using EmploymentSystem.Persistance.Repositories;
using EmploymentSystem.Services;
using EmploymentSystem.WebApi.Errors;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;

namespace EmploymentSystem.WebApi.Extentions;

public static class ApplicationServicesExtension
{
    public static IServiceCollection AddApplictaionServices(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddScoped<IUnitOfWork, UnitOfWork>();
        services.AddScoped<IVacancyRepository, VacancyRepository>();
        services.AddScoped<IApplicationRepository, ApplicationRepository>();

        services.Configure<ApiBehaviorOptions>(options =>
        {
            options.InvalidModelStateResponseFactory = (actioncontext) =>
            {
                var errors = actioncontext.ModelState.Where(p => p.Value.Errors.Count() > 0)//model state is a key valuepair of errors
                 .SelectMany(p => p.Value.Errors)
                 .Select(E => E.ErrorMessage)
                 .ToArray();
                var validationErrorResponse = new ApiValidationErrorResponse()
                { Errors = errors };
                return new BadRequestObjectResult(validationErrorResponse);
            };
        });
        var jwtSettings = new JwtSettings();
        configuration.Bind(JwtSettings.SectionName, jwtSettings);
        services.AddSingleton(Options.Create(jwtSettings));

        services.AddScoped<ITokenService, TokenService>();

        services.AddScoped<IVacancyServices, VacancyServices>();
        services.AddScoped<IApplicationServices, ApplicationServices>();

        return services;
    }
}
