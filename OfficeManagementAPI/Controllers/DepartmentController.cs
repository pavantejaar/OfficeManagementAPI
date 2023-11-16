using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using OfficeManagementAPI.Data;
using OfficeManagementAPI.Models;

namespace OfficeManagementAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DepartmentController : ControllerBase
    {
        private readonly OfficeDBContext dbContext;
        public DepartmentController(OfficeDBContext dbContext)
        {
            this.dbContext = dbContext;
        }

        [Authorize(Roles ="Reader,Writer")]
        [HttpGet]
        public IActionResult GetDepartments()
        {
            return Ok(dbContext.Department.ToList());
        }

        [Authorize(Roles = "Reader,Writer")]
        [HttpGet]
        [Route("{id:guid}")]
        public async Task<IActionResult> GetDepartment([FromRoute] Guid id)
        {
            var department = await dbContext.Department.FindAsync(id);
            if (department == null)
            {
                return NotFound();
            }
            return Ok(department);
        }

        [Authorize(Roles = "Writer")]
        [HttpPost]
        public async Task<IActionResult> AddDepartment(DepartmentDTO departmentDTO)
        {
            DateTime now = DateTime.Now;
            var department = new Department()
            {
                Id = Guid.NewGuid(),
                Name = departmentDTO.Name,
                CreatedDate = now,
                LastUpdatedDate = now
            };
            await dbContext.Department.AddAsync(department);
            await dbContext.SaveChangesAsync();
            return Ok(department);
        }

        [Authorize(Roles = "Reader,Writer")]
        [HttpPut]
        [Route("{id:guid}")]
        public async Task<IActionResult> UpdateDepartment([FromRoute] Guid id, DepartmentDTO departmentDTO)
        {
            DateTime now = DateTime.Now;
            var department = dbContext.Department.Find(id);
            if (department == null)
            {
                return NotFound();
            }
            department.Name = departmentDTO.Name;            
            department.LastUpdatedDate = now;
            await dbContext.SaveChangesAsync();
            return Ok(department);

        }

        /*[HttpDelete]
        [Route("{id:guid}")]
        public async Task<IActionResult> DeleteDepartment([FromRoute] Guid id)
        {
            var department = await dbContext.Department.FindAsync(id);
            if (department == null)
            {
                return NotFound();
            }
            dbContext.Remove(department);
            await dbContext.SaveChangesAsync();
            return Ok(department);
        }*/

    }
}
