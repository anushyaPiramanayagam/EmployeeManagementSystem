using EmployeeManagement.API.Services;
using EmployeeManagement.Application.DTOs;
using Microsoft.AspNetCore.Mvc;

namespace EmployeeManagement.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class LoginController : ControllerBase
{
    private readonly JwtService _jwtService;

    public LoginController(JwtService jwtService)
    {
        _jwtService = jwtService;
    }

    [HttpPost]
    public IActionResult Login(LoginDto loginDto)
    {
        if (loginDto.Username == "admin" &&
            loginDto.Password == "Admin@123")
        {
            var token =
                _jwtService.GenerateToken(loginDto.Username);

            return Ok(new
            {
                Token = token
            });
        }

        return Unauthorized("Invalid credentials.");
    }
}