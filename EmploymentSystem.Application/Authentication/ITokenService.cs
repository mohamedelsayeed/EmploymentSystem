using Microsoft.AspNetCore.Identity;

namespace EmploymentSystem.Application.Authentication;

public interface ITokenService
{
    string CreateToken(IdentityUser userManager);
}
