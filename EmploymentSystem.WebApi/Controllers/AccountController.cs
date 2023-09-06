using EmploymentSystem.Application.Authentication;
using EmploymentSystem.Domain.Entities;
using EmploymentSystem.WebApi.DTOs;
using EmploymentSystem.WebApi.Errors;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace EmploymentSystem.WebApi.Controllers;

public class AccountController : BaseApiController
{
    private readonly UserManager<User> _userManager;
    private readonly ITokenService _tokenService;
    public AccountController(UserManager<User> userManager, ITokenService tokenService)
    {
        _userManager = userManager;
        _tokenService = tokenService;
    }


    [HttpPost("register")]
    public async Task<ActionResult<UserDto>> Register(RegisterDto registerDto)
    {
        var checkEmail = await _userManager.FindByEmailAsync(registerDto.Email);
        if (checkEmail is not null)
            return BadRequest(new ApiValidationErrorResponse() { Errors = new string[] { "this email is already used" } });
        var user = new User()
        {
            FirstName = registerDto.FirstName,
            LastName = registerDto.LastName,
            DisplayName = registerDto.DisplayName,
            Email = registerDto.Email,
            PhoneNumber = registerDto.PhoneNumber,
            UserName = registerDto.Email.Split("@")[0],
            IsEmployeer = registerDto.IsEmployeer,
        };
        var result = await _userManager.CreateAsync(user, registerDto.Password);
        var identityUser = await _userManager.FindByEmailAsync(user.Email);
        if (identityUser is null)
            return BadRequest(new ApiValidationErrorResponse() { Errors = new string[] { "this email is already used" } });

        if (registerDto.IsEmployeer)
            await _userManager.AddToRoleAsync(identityUser, "Employer");
        else
            await _userManager.AddToRoleAsync(identityUser, "Applicant");

        var userRoles =await _userManager.GetRolesAsync(identityUser);
        var role = userRoles.FirstOrDefault();

        if (!result.Succeeded)
            return BadRequest(new ApiResponse(400));
        var token = _tokenService.CreateToken(identityUser, role);

        return Ok(new UserDto()
        {
            Token = token,
            Email = user.Email,
        });
    }

    [HttpPost("login")]
    public async Task<ActionResult<UserDto>> Login(LoginDto loginDto)
    {
        var user = await _userManager.FindByEmailAsync(loginDto.Email);

        if (user is null)
            return Unauthorized(new ApiResponse(401));
        var checkpassword = await _userManager.CheckPasswordAsync(user, loginDto.Password);
        if (!checkpassword)
            return Unauthorized(new ApiResponse(401));
        var userRoles = await _userManager.GetRolesAsync(user);
        var role  = userRoles.FirstOrDefault(); 
        var token = _tokenService.CreateToken(user, role);

        return Ok(new UserDto()
        {
            Token = token,
            Email = user.Email,
        }); ;
    }
}