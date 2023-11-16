using Azure;
using Google.Apis.Admin.Directory.directory_v1.Data;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using OfficeManagementAPI.Data;
using OfficeManagementAPI.Models;
using OfficeManagementAPI.Repositories;
using static Google.Apis.Requests.BatchRequest;

namespace OfficeManagementAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmployeeController : ControllerBase
    {
        private readonly OfficeDBContext dbContext;
        private readonly IEmployeeRepository employeeRepository;
        public EmployeeController(OfficeDBContext dbContext, IEmployeeRepository employeeRepository)
        {
            this.dbContext = dbContext;
            this.employeeRepository = employeeRepository;
        }

        [Authorize(Roles = "Reader,Writer")]
        [HttpGet]
        public async Task<IActionResult> GetEmployees()
        {
            var employees = await employeeRepository.GetEmployeesAsync();
            return Ok(employees);
        }

        [Authorize(Roles = "Reader,Writer")]
        [HttpGet]
        [Route("{id:guid}")]
        public async Task<IActionResult> GetEmployee([FromRoute] Guid id)
        {
            var employee = await employeeRepository.GetEmployeeAsync(id);
            return Ok(employee);
        }

        [Authorize(Roles = "Writer,Reader")]
        [HttpPost]
        public async Task<IActionResult> AddEmployees(EmployeeDTO employeeDTO)
        {
            DateTime now = DateTime.Now;
            //Creating Employee Table entry
            var employee = new Employee()
            {
                Id = Guid.NewGuid(),
                Name = employeeDTO.Name,
                Contact = employeeDTO.Contact,
                Status = employeeDTO.Status,
                CreatedDate = now,
                LastUpdatedDate = now
            };
            await dbContext.Employees.AddAsync(employee);
            await dbContext.SaveChangesAsync();          

            //Creating Department Table entry
            var deptcheck = await dbContext.Department.Where(d => d.Name == employeeDTO.Department.Name).FirstOrDefaultAsync();
            //if department does not exist
            if (deptcheck == null)
            {
                var department = new Department()
                {
                    Id = Guid.NewGuid(),
                    Name = employeeDTO.Department.Name,
                    CreatedDate = now,
                    LastUpdatedDate = now
                };
                await dbContext.Department.AddAsync(department);
                await dbContext.SaveChangesAsync();

                //Creating Association Table entry
                var newassociation = new EmployeeDepartmentAssociation()
                {
                    Id = Guid.NewGuid(),
                    EmployeeId = employee.Id,
                    DepartmentId = department.Id,
                    CreatedOn = now,
                    ModifiedOn = now,
                };
                await dbContext.EmployeeDepartmentAssociation.AddAsync(newassociation);
                await dbContext.SaveChangesAsync();
            }
            else
            {
                //if dept already exists then create association directly
                //Creating Association Table entry
                var existingassociation = new EmployeeDepartmentAssociation()
                {
                    Id = Guid.NewGuid(),
                    EmployeeId = employee.Id,
                    DepartmentId = deptcheck.Id,
                    CreatedOn = now,
                    ModifiedOn = now,
                };
                await dbContext.EmployeeDepartmentAssociation.AddAsync(existingassociation);
                await dbContext.SaveChangesAsync();
            }
            return Ok(employee);
        }

        [Authorize(Roles = "Writer,Reader")]
        [HttpPut]
        [Route("{id:guid}")]
        public async Task<IActionResult> UpdateEmployee([FromRoute] Guid id, EmployeeDTO employeeDTO)
        {
            DateTime now = DateTime.Now;
            var employee = dbContext.Employees.Find(id);
            if(employee == null) 
            {
                return NotFound();
            }
            employee.Name = employeeDTO.Name;
            employee.Contact = employeeDTO.Contact;
            employee.Status = employeeDTO.Status;
            employee.LastUpdatedDate = now;
            await dbContext.SaveChangesAsync();
            return Ok(employee);

        }

        [Authorize(Roles = "Writer,Reader")]
        [HttpDelete]
        [Route("{id:guid}")]
        public async Task<IActionResult> DeleteEmployee([FromRoute] Guid id)
        {
            var employee = await dbContext.Employees.FindAsync(id);
            if (employee == null)
            {
               return NotFound();
            }
            dbContext.Remove(employee);
            await dbContext.SaveChangesAsync();
            return Ok(employee);
        }

        /*[Authorize]
        [HttpGet]
        [Route("GetData")]
        public string GetData()
        {
            return "Authenticated with JWT";
        }

        [HttpGet]
        [Route("Details")]
        public string Details()
        {
            return "Authenticated with JWT";
        }

        [Authorize]
        [HttpPost]
        public string AddUser(Users user)
        {
            return "User added with username " + user.UserName;
        }*/

    }
}
