using Asp.Versioning;
using EmployeeManagement.Application.Common;
using EmployeeManagement.Application.DTOs;
using EmployeeManagement.Application.Interfaces;
using EmployeeManagement.Application.Services;
using EmployeeManagement.Domain.Entities;
using Microsoft.AspNetCore.Authorization;   
using Microsoft.AspNetCore.Mvc;
namespace EmployeeManagement.API.Controllers;

[ApiController]
[ApiVersion("1.0")]
[Route("api/v{version:apiVersion}/[controller]")]
[Authorize]
public class EmployeesController : ControllerBase
{
    private readonly IEmployeeService _employeeService;
    private readonly ILogger<EmployeesController> _logger;
   public EmployeesController(
    IEmployeeService employeeService,
    ILogger<EmployeesController> logger)
{
    _employeeService =
        employeeService;

    _logger = logger;
}

    [Authorize]
    [HttpGet]
    public async Task<IActionResult> GetAllEmployees()
    {
        _logger.LogInformation("Fetching all employees");
        var employees = await _employeeService.GetAllAsync();

        return Ok(new ApiResponse<IEnumerable<EmployeeDto>>
  (
      true,
      "Employees retrieved successfully.",
      employees
  ));
    }

    [Authorize]
    [HttpGet("{id}")]
    public async Task<IActionResult> GetEmployeeById(int id)
    {
        var employee = await _employeeService.GetByIdAsync(id);

        if (employee == null)
            return NotFound();

        return Ok(new ApiResponse<EmployeeDto>
 (
     true,
     "Employee retrieved successfully.",
     employee
 ));
    }
    [Authorize]
    [HttpGet("search")]
    public async Task<IActionResult> SearchEmployees(
    [FromQuery] QueryParameters parameters)
    {
        _logger.LogInformation(
            "Searching employees");

        var employees =
            await _employeeService.GetEmployeesAsync(parameters);

        return Ok(employees);
    }

    [Authorize(Roles = "Admin,HR")]
    [HttpPost]
    public async Task<IActionResult> CreateEmployee(
      CreateEmployeeDto request)
    {
        var employee =
            await _employeeService.CreateAsync(request);

        return CreatedAtAction(
    nameof(GetEmployeeById),
    new { id = employee.Id },
    new ApiResponse<EmployeeDto>
    (
        true,
        "Employee created successfully.",
        employee
    ));
    }
    [Authorize(Roles = "Admin,HR")]
    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateEmployee(
     int id,
     UpdateEmployeeDto request)
    {
        var employee =
            await _employeeService.UpdateAsync(id, request);

        if (employee == null)
            return NotFound();

        return Ok(new ApiResponse<string>
(
    true,
    "Employee updated successfully.",
    null
));
    }
    [Authorize(Roles = "Admin")]
    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteEmployee(int id)
    {
        var deleted =
            await _employeeService.DeleteAsync(id);

        if (!deleted)
            return NotFound();

        return Ok(new ApiResponse<string>
(
    true,
    "Employee deleted successfully.",
    null
));
    }
}