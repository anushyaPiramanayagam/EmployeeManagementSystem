using EmployeeManagement.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using EmployeeManagement.Application.Interfaces;
using EmployeeManagement.Infrastructure.Repositories;
using EmployeeManagement.Application.Services;

namespace EmployeeManagement.Infrastructure.Configurations;

public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructure(
        this IServiceCollection services,
        IConfiguration configuration)
    {
        services.AddDbContext<ApplicationDbContext>(options =>
            options.UseSqlServer(
                configuration.GetConnectionString("DefaultConnection")));
        services.AddScoped<IEmployeeRepository, EmployeeRepository>();

        services.AddScoped<IDepartmentRepository, DepartmentRepository>();
        services.AddScoped<IEmployeeService, EmployeeService>();

        return services;
    }
}