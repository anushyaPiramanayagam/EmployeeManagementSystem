using EmployeeManagement.Application.Interfaces;
using Microsoft.AspNetCore.Mvc;
using EmployeeManagement.Application.DTOs;
using EmployeeManagement.Domain.Entities;

namespace EmployeeManagement.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class EmployeesController : ControllerBase
{
    private readonly IEmployeeRepository _employeeRepository;

    public EmployeesController(IEmployeeRepository employeeRepository)
    {
        _employeeRepository = employeeRepository;
    }

    [HttpGet]
    public async Task<IActionResult> GetAllEmployees()
    {
        var employees = await _employeeRepository.GetAllAsync();

        return Ok(employees);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetEmployeeById(int id)
    {
        var employee = await _employeeRepository.GetByIdAsync(id);

        if (employee == null)
            return NotFound();

        return Ok(employee);
    }
    [HttpPost]
    public async Task<IActionResult> CreateEmployee(
    CreateEmployeeDto request)
    {
        var employee = new Employee
        {
            EmployeeCode = request.EmployeeCode,
            FirstName = request.FirstName,
            LastName = request.LastName,
            Email = request.Email,
            Designation = request.Designation,
            DepartmentId = request.DepartmentId
        };

        await _employeeRepository.AddAsync(employee);

        return Ok(employee);
    }
    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateEmployee(
    int id,
    UpdateEmployeeDto request)
    {
        var employee = await _employeeRepository.GetByIdAsync(id);

        if (employee == null)
            return NotFound();

        employee.FirstName = request.FirstName;
        employee.LastName = request.LastName;
        employee.Email = request.Email;
        employee.Designation = request.Designation;
        employee.DepartmentId = request.DepartmentId;

        await _employeeRepository.UpdateAsync(employee);

        return Ok(employee);
    }
    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteEmployee(int id)
    {
        var employee = await _employeeRepository.GetByIdAsync(id);

        if (employee == null)
            return NotFound();

        await _employeeRepository.DeleteAsync(id);

        return NoContent();
    }
}