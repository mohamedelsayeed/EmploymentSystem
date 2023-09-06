using EmploymentSystem.Domain.Entities;

namespace EmploymentSystem.Application.Authentication;

public interface ITokenService
{
    string CreateToken(User userManager, string role);
}
