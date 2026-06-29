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
        if (loginDto.Username == "admin"
     && loginDto.Password == "Admin@123")
        {
            var token =
                _jwtService.GenerateToken(
                    loginDto.Username,
                    "Admin");

            return Ok(new { Token = token });
        }

        if (loginDto.Username == "hr"
            && loginDto.Password == "Hr@123")
        {
            var token =
                _jwtService.GenerateToken(
                    loginDto.Username,
                    "HR");

            return Ok(new { Token = token });
        }

        if (loginDto.Username == "employee"
            && loginDto.Password == "Employee@123")
        {
            var token =
                _jwtService.GenerateToken(
                    loginDto.Username,
                    "Employee");

            return Ok(new { Token = token });
        }

        return Unauthorized();

    }
}