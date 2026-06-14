using EmployeeManagement.Application.DTOs;

namespace EmployeeManagement.Application.Services;

public interface IEmployeeService
{
    Task<IEnumerable<EmployeeDto>> GetAllAsync();

    Task<EmployeeDto?> GetByIdAsync(int id);

    Task<EmployeeDto> CreateAsync(CreateEmployeeDto request);

    Task<EmployeeDto?> UpdateAsync(int id, UpdateEmployeeDto request);

    Task<bool> DeleteAsync(int id);
}