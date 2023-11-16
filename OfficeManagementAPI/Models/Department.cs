using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace OfficeManagementAPI.Models
{
    public class Department
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid Id { get; set; }
        [Required(ErrorMessage = "Enter Department Name")]
        public string Name { get; set; }       
        public DateTime CreatedDate { get; set; }

        public DateTime LastUpdatedDate { get; set; }
        public virtual ICollection<EmployeeDepartmentAssociation> EmployeeAssociations { get; set; }
    }
}
