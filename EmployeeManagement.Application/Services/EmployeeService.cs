using EmployeeManagement.Application.DTOs;
using EmployeeManagement.Application.Interfaces;
using EmployeeManagement.Domain.Entities;
using AutoMapper;
namespace EmployeeManagement.Application.Services;
using Microsoft.Extensions.Caching.Memory;
public class EmployeeService : IEmployeeService
{
    private readonly IEmployeeRepository _employeeRepository;
    private readonly IMapper _mapper;
    private readonly IMemoryCache _memoryCache;
    public EmployeeService(
     IEmployeeRepository employeeRepository,
     IMapper mapper,
     IMemoryCache memoryCache)
    {
        _employeeRepository = employeeRepository;
        _mapper = mapper;
        _memoryCache = memoryCache;
    }

    public async Task<IEnumerable<EmployeeDto>> GetAllAsync()
    {
        const string cacheKey = "employees";

        if (_memoryCache.TryGetValue(cacheKey, out IEnumerable<EmployeeDto>? employees))
        {
            return employees!;
        }

        var employeeEntities = await _employeeRepository.GetAllAsync();

        employees = _mapper.Map<IEnumerable<EmployeeDto>>(employeeEntities);

        var cacheOptions = new MemoryCacheEntryOptions()
            .SetAbsoluteExpiration(TimeSpan.FromMinutes(5));

        _memoryCache.Set(cacheKey, employees, cacheOptions);
        _memoryCache.Remove("employees");
        return employees;
    }
    public async Task<IEnumerable<EmployeeDto>> GetEmployeesAsync(QueryParameters parameters)
    {
        var employees =
            await _employeeRepository.GetEmployeesAsync(parameters);

        return _mapper.Map<IEnumerable<EmployeeDto>>(employees);
    }

    public async Task<EmployeeDto?> GetByIdAsync(int id)
    {
        var employee = await _employeeRepository.GetByIdAsync(id);

        if (employee == null)
            return null;

        return _mapper.Map<EmployeeDto>(employee);
    }

    public async Task<EmployeeDto> CreateAsync(CreateEmployeeDto request)
    {
        var employee =
     _mapper.Map<Employee>(request);

        await _employeeRepository.AddAsync(employee);

        return _mapper.Map<EmployeeDto>(employee);
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