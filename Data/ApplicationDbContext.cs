using EmployeesWebAPI.Data.Entities;
using Microsoft.EntityFrameworkCore;

namespace EmployeesWebAPI.Data
{
    public class ApplicationDbContext : DbContext
    {
        public DbSet<Employee> Employees { get; set; }

        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Employee>()
                        .HasIndex(e => e.Email)
                        .IsUnique();
        }
    }
}
