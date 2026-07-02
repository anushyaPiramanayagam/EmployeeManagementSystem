namespace EmployeeManagement.Infrastructure.UnitOfWork;

public interface IUnitOfWork
{
    Task<int> SaveChangesAsync();
}