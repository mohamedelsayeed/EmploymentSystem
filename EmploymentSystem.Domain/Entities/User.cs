using EmploymentSystem.Domain.Common;
using Microsoft.AspNetCore.Identity;

namespace EmploymentSystem.Domain.Entities;

public class User :IdentityUser
{
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string DisplayName { get; set; }
    public bool IsEmployeer { get; set; }
}
