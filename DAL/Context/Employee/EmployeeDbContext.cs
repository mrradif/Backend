using Microsoft.EntityFrameworkCore;
using Shared.Domain.Employee;

namespace DAL.Context.Employee
{
    public class EmployeeDbContext : DbContext
    {
        public EmployeeDbContext(DbContextOptions<EmployeeDbContext> options) : base(options)
        {
            // Optionally, apply migrations on startup
            // this.Database.Migrate();
        }

        // DbSet for Employee Information
        public DbSet<EmployeeInformation> HR_EmployeeInformation { get; set; }
    }
}
