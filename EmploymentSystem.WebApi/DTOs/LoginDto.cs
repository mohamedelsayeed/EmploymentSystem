using System.ComponentModel.DataAnnotations;

namespace EmploymentSystem.WebApi.DTOs;

public class LoginDto
{
    [Required]
    [EmailAddress]
    public string Email { get; set; }
   
    [Required]
    public string Password { get; set; }
}
