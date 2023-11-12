using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using OfficeManagementAPI.Models;

namespace OfficeManagementAPI.Data
{
    public class OfficeDBContext : DbContext
    {
        public OfficeDBContext(DbContextOptions<OfficeDBContext> dbContextOptions) : base(dbContextOptions)
        { 
        }
        public DbSet<Users> User { get; set; }
        public DbSet<Employee> Employees { get; set; }
        
        
        /*public DbSet<Employee> Employees { get; set; }
        public DbSet<Department> Departments { get; set; }
        public DbSet<EmployeeDepartmentAssociation> employeeDepartmentAssociations { get; set; }*/

        /*protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Employee>()
                .ToTable("Employees")
                .HasKey(e => e.Id);



            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Department>()
                .ToTable("Departments")
                .HasKey(e => e.Id);
            base.OnModelCreating(modelBuilder);
        }*/
    }
}




