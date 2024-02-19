using Microsoft.AspNetCore.Identity;
using InternshipProjectMini.Constants;
using System.Runtime.CompilerServices;
using System.Security.Claims;
using System.Threading.Tasks;
using System.Linq;
using System;
using InternshipProjectMini.Models;

namespace InternshipProjectMini.Seeds
{
    public static class DefaultUsers
    {
        public static async Task SeedSuperAdminAsync(UserManager<ApplicationUser> userManager, RoleManager<IdentityRole> roleManager)
        {
            var defaultUser = new ApplicationUser
            {
                UserName = "superadmin@gmail.com",
                Email = "superadmin@gmail.com",
                EmailConfirmed = true,
                SelectedPermissions = $"{Permissions.Employee.Details},{Permissions.Employee.Create},{Permissions.Employee.Edit},{Permissions.Employee.Delete}," +
                                      $"{Permissions.Location.Details},{Permissions.Location.Create},{Permissions.Location.Edit},{Permissions.Location.Delete}," +
                                      $"{Permissions.Department.Details},{Permissions.Department.Create},{Permissions.Department.Edit},{Permissions.Department.Delete}," +
                                      $"{Permissions.Machine.Details},{Permissions.Machine.Create},{Permissions.Machine.Edit},{Permissions.Machine.Delete}",
            };

            if (userManager.Users.All(u => u.Id != defaultUser.Id))
            {
                var user = await userManager.FindByEmailAsync(defaultUser.Email);
                if (user == null)
                {
                    await userManager.CreateAsync(defaultUser, "123Password!");
                    await userManager.AddToRoleAsync(defaultUser, Roles.Basic.ToString());
                    await userManager.AddToRoleAsync(defaultUser, Roles.Admin.ToString());
                    await userManager.AddToRoleAsync(defaultUser, Roles.SuperAdmin.ToString());
                }
                await roleManager.SeedClaimsForSuperAdmin(defaultUser);
            }
        }

        private async static Task SeedClaimsForSuperAdmin(this RoleManager<IdentityRole> roleManager, ApplicationUser defaultUser)
        {
            var adminRole = await roleManager.FindByNameAsync("SuperAdmin");
            await roleManager.AddPermissionClaim(adminRole, "Products");
        }
        public static async Task AddPermissionClaim(this RoleManager<IdentityRole> roleManager, IdentityRole role, string module)
        {
            var allClaims = await roleManager.GetClaimsAsync(role);
            var allPermissions = Permissions.GeneratePermissionsForModule(module);
            foreach (var permission in allPermissions)
            {
                if (!allClaims.Any(a => a.Type == "Permission" && a.Value == permission))
                {
                    await roleManager.AddClaimAsync(role, new System.Security.Claims.Claim("Permission", permission));
                }
            }
        }
        public static async Task SeedBasicUserAsync(UserManager<ApplicationUser> userManager, RoleManager<IdentityRole> roleManager)
        {
            var defaultUser = new ApplicationUser

            {
                UserName = "basicuser@gmail.com",
                Email = "basicuser@gmail.com",
                EmailConfirmed = true
            };
            if (userManager.Users.All(u => u.Id != defaultUser.Id))
            {
                var user = await userManager.FindByEmailAsync(defaultUser.Email);
                if (user == null)
                {
                    await userManager.CreateAsync(defaultUser, "123Password!");
                    await userManager.AddToRoleAsync(defaultUser, Roles.Basic.ToString());
                }
            }
        }
        
    }
}

