namespace OfficeManagementAPI.Models
{
    public class EmployeeDTO
    {
        public string Name { get; set; }
        public long Contact { get; set; }
        public string Status { get; set; }
        public DepartmentDTO Department { get; set; }
    }
}
