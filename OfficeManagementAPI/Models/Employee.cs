namespace OfficeManagementAPI.Models
{
    public class Employee
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public long Contact { get; set; }
        public string Status { get; set; }
    }
}
