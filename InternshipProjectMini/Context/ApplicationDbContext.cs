using InternshipProjectMini.Models;
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

        public DbSet<Employee> Employees { get; set; }
        public DbSet<Department> Departments { get; set; }
        public DbSet<Location> Locations { get; set; }
        public DbSet<Machine> Machines { get; set; }
        public DbSet<UserPermissions> UserPermissions { get; set; }
        public DbSet<RoleViewModel> RoleViewModel { get; set; }
        public DbSet<UserViewModel> UserViewModel { get; set; }
        public DbSet<UserPermissionViewModel> UserPermissionViewModel { get; set; }
        public DbSet<RolePermission> RolePermission { get; set; }
        public DbSet<UserRolesViewModel> UserRolesViewModels { get; set; }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<UserPermissions>()
                .HasKey(up => up.UserId);

            modelBuilder.Entity<UserPermissions>()
                .HasOne(up => up.User)
                .WithOne(u => u.UserPermissions)
                .HasForeignKey<UserPermissions>(up => up.UserId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<UserRolesViewModel>().HasNoKey();

        }
  
    }
}
