using InternshipProjectMini.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace InternshipProjectMini.Context
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        public DbSet<ApplicationUser> ApplicationUsers { get; set; }
        public DbSet<IdentityRole> IdentityRoles { get; set; }
        public DbSet<IdentityUserClaim<string>> IdentityUserClaims { get; set; }
        public DbSet<IdentityUserRole<string>> IdentityUserRoles { get; set; }
        public DbSet<IdentityUserLogin<string>> IdentityUserLogins { get; set; }
        public DbSet<IdentityUserToken<string>> IdentityUserTokens { get; set; }
        public DbSet<IdentityRoleClaim<string>> IdentityRoleClaims { get; set; }

        public DbSet<Employee> Employees { get; set; }
        public DbSet<Department> Departments { get; set; }
        public DbSet<Location> Locations { get; set; }
        public DbSet<Machine> Machines { get; set; }
        public DbSet<Module> Modules { get; set; }
        public DbSet<UserPermission> UserPermissions { get; set; }
        
    }
}
