
using Microsoft.AspNetCore.Identity;

namespace EmploymentSystem.Application.Absctractions;

public interface ITokenService
{
    string CreateToken(IdentityUser userManager);
}
