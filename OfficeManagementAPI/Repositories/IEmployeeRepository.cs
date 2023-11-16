using OfficeManagementAPI.Models;

namespace OfficeManagementAPI.Repositories
{
    public interface IEmployeeRepository
    {
        Task<List<Employee>> GetEmployeesAsync();

        Task<Employee?> GetEmployeeAsync(Guid id);

        //Task<Employee> AddEmployeesAsync();
    }
}
