using EmploymentSystem.Application.Authentication;
using EmploymentSystem.Domain.Entities;
using EmploymentSystem.Persistance.Context;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.Text;

namespace EmploymentSystem.WebApi.Extentions;

public static class IdentityServicesExtensions
{
    public static IServiceCollection AddIdentityServices(this IServiceCollection services, IConfiguration configuration, JwtSettings jwtSettings)
    {   


    services.AddDbContext<AppIdentityDBContext>(options =>
            {
                options.UseSqlServer("Data Source=.;Initial Catalog=EmploymentSystemDb;Integrated Security=True;Connect Timeout=30;Encrypt=False;");
            });
        services.AddIdentity<User, IdentityRole<int>>()
            .AddEntityFrameworkStores<AppIdentityDBContext>();
            //.AddDefaultTokenProviders();
        


        services.AddAuthentication(options =>
        {
            options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
            options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
        })
           .AddJwtBearer(options =>
           {
               options.TokenValidationParameters = new TokenValidationParameters()
               {
                   ValidateIssuer = true,
                   ValidIssuer = jwtSettings.Issuer,
                   ValidateAudience = true,
                   ValidAudience = jwtSettings.Audiance,
                   ValidateLifetime = true,
                   ValidateIssuerSigningKey = true,
                   IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtSettings.Secret))
               };
           });
      
        return services;
    }
}
