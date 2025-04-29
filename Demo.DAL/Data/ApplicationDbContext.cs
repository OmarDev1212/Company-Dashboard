using Demo.DAL.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System.Reflection;

namespace Demo.DAL.Data
{
    public class ApplicationDbContext(DbContextOptions options) :/*DbContext*/IdentityDbContext<ApplicationUser>(options)
    {
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            //modelBuilder.ApplyConfiguration<Department>(new DeparmentConfigurations());

            modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
            modelBuilder.Entity<ApplicationUser>().ToTable("Users", "Security");
            modelBuilder.Entity<IdentityRole>().ToTable("Roles", "Security");
            modelBuilder.Entity<IdentityUserRole<string>>().ToTable("UserRoles", "Security");
            modelBuilder.Entity<IdentityUserToken<string>>().ToTable("UserTokens", "Security");
            modelBuilder.Entity<IdentityUserLogin<string>>().ToTable("UserLogins", "Security");
            modelBuilder.Entity<IdentityUserClaim<string>>().ToTable("UserCliams", "Security");
            modelBuilder.Entity<IdentityRoleClaim<string>>().ToTable("RoleCliams", "Security");
        }
        public DbSet<Department> Departments { get; set; }
        public DbSet<Employee> Employees { get; set; }
    }
}
