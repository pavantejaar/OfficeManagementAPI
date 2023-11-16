using Microsoft.EntityFrameworkCore;
using OfficeManagementAPI.Data;
using OfficeManagementAPI.Models;

namespace OfficeManagementAPI.Repositories
{
    public class SQLEmployeeRepository : IEmployeeRepository
    {
        private readonly OfficeDBContext dbContext;
        public SQLEmployeeRepository(OfficeDBContext dbContext) 
        {
            this.dbContext = dbContext;
        }
        public async Task<List<Employee>> GetEmployeesAsync()
        {
           return await dbContext.Employees.ToListAsync();
            //return await dbContext.Employees.Include("Department").ToListAsync(); 
            //throw new NotImplementedException();
        }

        public async Task<Employee?> GetEmployeeAsync(Guid id)
        {
            return await dbContext.Employees.FindAsync(id);           
        }       
    }
}
