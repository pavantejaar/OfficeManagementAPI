using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using OfficeManagementAPI.Data;
using OfficeManagementAPI.Models;

namespace OfficeManagementAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmployeeController : ControllerBase
    {
        private readonly OfficeDBContext dbContext;
        public EmployeeController(OfficeDBContext dbContext)
        {
            this.dbContext = dbContext;
        }

        //[Authorize]
        [HttpGet]
        public IActionResult GetEmployees()
        {
            return Ok(dbContext.Employees.ToList());
        }

        [HttpGet]
        [Route("{id:guid}")]
        public async Task<IActionResult> GetEmployee([FromRoute] Guid id)
        {
            var employee = await dbContext.Employees.FindAsync(id);
            if(employee == null)
            {
                return NotFound();
            }
            return Ok(employee);
        }

        [HttpPost]
        public async Task<IActionResult> AddEmployees(EmployeeDTO employeeDTO)
        {
            var employee = new Employee()
            {
                Id = Guid.NewGuid(),
                Name = employeeDTO.Name,
                Contact = employeeDTO.Contact,
                Status = employeeDTO.Status
            };
            await dbContext.Employees.AddAsync(employee);
            await dbContext.SaveChangesAsync();
            return Ok(employee);
        }

        [HttpPut]
        [Route("{id:guid}")]
        public async Task<IActionResult> UpdateEmployee([FromRoute] Guid id, EmployeeDTO employeeDTO)
        {
            var employee = dbContext.Employees.Find(id);
            if(employee == null) 
            {
                return NotFound();
            }
            employee.Name = employeeDTO.Name;
            employee.Contact = employeeDTO.Contact;
            employee.Status = employeeDTO.Status;
            await dbContext.SaveChangesAsync();
            return Ok(employee);

        }

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
