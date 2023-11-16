using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace OfficeManagementAPI.Models
{
    public class EmployeeDepartmentAssociation
    {
        public Guid Id { get; set; }

        [Required(ErrorMessage = "Enter Employee Id")]
        [ForeignKey("Employee")]
        public Guid EmployeeId { get; set; }

        [Required(ErrorMessage = "Enter Department Id")]
        [ForeignKey("Department")]
        public Guid DepartmentId { get; set; }

        public DateTime CreatedOn { get; set; }

        public DateTime ModifiedOn { get; set; }
    }
}
