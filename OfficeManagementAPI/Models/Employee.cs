using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace OfficeManagementAPI.Models
{
    public class Employee
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid Id { get; set; }
        [Required(ErrorMessage = "Enter Name")]
        public string Name { get; set; }
        [Required(ErrorMessage = "Enter Contact Number")]
        [Phone(ErrorMessage = "Enter a valid contact number")]
        public long Contact { get; set; }
        [Required(ErrorMessage = "Enter Status")]
        public string Status { get; set; }
        
        public DateTime CreatedDate { get; set; }
       
        public DateTime LastUpdatedDate { get; set;} 
    }
}

