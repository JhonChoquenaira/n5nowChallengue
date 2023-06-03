using N5NowChallengue.DataService.Models;
using Microsoft.EntityFrameworkCore;

namespace N5NowChallengue.DataService.Context
{
   public class ApplicationDbContext : DbContext
    {
        public DbSet<Employee> Employees { get; set; }
        public DbSet<PermissionType> PermissionTypes { get; set; }
        public DbSet<Permission> Permissions { get; set; }
        public DbSet<EmployeePermission> EmployeePermissions { get; set; }
        public DbSet<EmployeePermissionType> EmployeePermissionTypes { get; set; }
        public DbSet<PermissionTypePermission> PermissionTypePermissions { get; set; }

        public ApplicationDbContext()
        {
        }
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
        }
        
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<EmployeePermission>()
                .HasKey(ep => new { ep.EmployeeId, ep.PermissionId });
            modelBuilder.Entity<EmployeePermissionType>()
                .HasKey(ept => new { ept.EmployeeId, ept.PermissionTypeId });
            modelBuilder.Entity<PermissionTypePermission>()
                .HasKey(ep => new { ep.PermissionTypeId, ep.PermissionId });

        }
        

    }

}
