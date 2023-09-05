using EmploymentSystem.Application.Absctractions;
using EmploymentSystem.Application.Authentication;
using EmploymentSystem.Persistance.Authentication;
using EmploymentSystem.WebApi.Errors;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;

namespace EmploymentSystem.WebApi.Extentions
{
    public static class ApplicationServicesExtension
    {
        public static IServiceCollection AddApplictaionServices(this IServiceCollection services, IConfiguration configuration)
        {
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
            return services;
        }
    }
}
