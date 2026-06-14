using EmployeeManagement.Application.DTOs;
using EmployeeManagement.Application.Interfaces;
using EmployeeManagement.Domain.Entities;

namespace EmployeeManagement.Application.Services;

public class EmployeeService : IEmployeeService
{
    private readonly IEmployeeRepository _employeeRepository;

    public EmployeeService(
        IEmployeeRepository employeeRepository)
    {
        _employeeRepository = employeeRepository;
    }

    public async Task<IEnumerable<EmployeeDto>> GetAllAsync()
    {
        var employees = await _employeeRepository.GetAllAsync();

        return employees.Select(e => new EmployeeDto
        {
            Id = e.Id,
            EmployeeCode = e.EmployeeCode,
            FirstName = e.FirstName,
            LastName = e.LastName,
            Email = e.Email,
            Designation = e.Designation,
            DepartmentName = e.Department?.Name ?? ""
        });
    }

    public async Task<EmployeeDto?> GetByIdAsync(int id)
    {
        var employee = await _employeeRepository.GetByIdAsync(id);

        if (employee == null)
            return null;

        return new EmployeeDto
        {
            Id = employee.Id,
            EmployeeCode = employee.EmployeeCode,
            FirstName = employee.FirstName,
            LastName = employee.LastName,
            Email = employee.Email,
            Designation = employee.Designation,
            DepartmentName = employee.Department?.Name ?? ""
        };
    }

    public async Task<EmployeeDto> CreateAsync(CreateEmployeeDto request)
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

        return new EmployeeDto
        {
            Id = employee.Id,
            EmployeeCode = employee.EmployeeCode,
            FirstName = employee.FirstName,
            LastName = employee.LastName,
            Email = employee.Email,
            Designation = employee.Designation
        };
    }

    public async Task<EmployeeDto?> UpdateAsync(
        int id,
        UpdateEmployeeDto request)
    {
        var employee = await _employeeRepository.GetByIdAsync(id);

        if (employee == null)
            return null;

        employee.FirstName = request.FirstName;
        employee.LastName = request.LastName;
        employee.Email = request.Email;
        employee.Designation = request.Designation;
        employee.DepartmentId = request.DepartmentId;

        await _employeeRepository.UpdateAsync(employee);

        return new EmployeeDto
        {
            Id = employee.Id,
            EmployeeCode = employee.EmployeeCode,
            FirstName = employee.FirstName,
            LastName = employee.LastName,
            Email = employee.Email,
            Designation = employee.Designation
        };
    }

    public async Task<bool> DeleteAsync(int id)
    {
        var employee = await _employeeRepository.GetByIdAsync(id);

        if (employee == null)
            return false;

        await _employeeRepository.DeleteAsync(id);

        return true;
    }
}