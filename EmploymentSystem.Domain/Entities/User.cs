using EmploymentSystem.Domain.Common;
using Microsoft.AspNet.Identity.EntityFramework;

namespace EmploymentSystem.Domain.Entities;

public class User :BaseEntity
{
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string Email { get; set; }
    public string DisplayName { get; set; }
    public bool IsEmployeer { get; set; }
}
