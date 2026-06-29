using EmployeeManagement.Application.DTOs;
using EmployeeManagement.Application.Interfaces;
using EmployeeManagement.Application.Services;
using EmployeeManagement.Domain.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;   
namespace EmployeeManagement.API.Controllers;

[ApiController]
[Route("api/[controller]")]
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

    [HttpGet]
    public async Task<IActionResult> GetAllEmployees()
    {
        _logger.LogInformation("Fetching all employees");
        var employees = await _employeeService.GetAllAsync();

        return Ok(employees);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetEmployeeById(int id)
    {
        var employee = await _employeeService.GetByIdAsync(id);

        if (employee == null)
            return NotFound();

        return Ok(employee);
    }
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

    [HttpPost]
    public async Task<IActionResult> CreateEmployee(
      CreateEmployeeDto request)
    {
        var employee =
            await _employeeService.CreateAsync(request);

        return Ok(employee);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateEmployee(
     int id,
     UpdateEmployeeDto request)
    {
        var employee =
            await _employeeService.UpdateAsync(id, request);

        if (employee == null)
            return NotFound();

        return Ok(employee);
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteEmployee(int id)
    {
        var deleted =
            await _employeeService.DeleteAsync(id);

        if (!deleted)
            return NotFound();

        return NoContent();
    }
}