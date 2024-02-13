using Microsoft.AspNetCore.Identity;
using InternshipProjectMini.Constants;
using System.Runtime.CompilerServices;
using System.Security.Claims;

namespace InternshipProjectMini.Seeds
{
    public static class DefaultUsers
    {
        public static async Task SeedDeveloperAsync(UserManager<IdentityUser> userManager,
            RoleManager<IdentityRole> roleManager)
        {
            var defaultUser = new IdentityUser
            {
                UserName = "aksa@gmail.com",
                Email = "aksa@gmail.com",
                EmailConfirmed = true,
                PhoneNumberConfirmed = true,
            };
            if (userManager.Users.All(u => u.Id != defaultUser.Id))
            {
                var user = await userManager.FindByEmailAsync(defaultUser.Email);
                if (user != null)
                {
                    await userManager.CreateAsync(defaultUser, "123Pa$$word!");
                    await userManager.AddToRoleAsync(defaultUser, Roles.Developer.ToString());
                }
            }
        }

        public static async Task SeedAdministrationAsync(UserManager<IdentityUser> userManager,
          RoleManager<IdentityRole> roleManager)
        {
            var defaultUser = new IdentityUser
            {
                UserName = "admin@gmail.com",
                Email = "admin@gmail.com",
                EmailConfirmed = true,
                PhoneNumberConfirmed = true,
            };
            if (userManager.Users.All(u => u.Id != defaultUser.Id))
            {
                var user = await userManager.FindByEmailAsync(defaultUser.Email);
                if (user != null)
                {
                    await userManager.CreateAsync(defaultUser, "123Pa$$word!");
                    await userManager.AddToRoleAsync(defaultUser, Roles.Administration.ToString());
                    await userManager.AddToRoleAsync(defaultUser, Roles.Manager.ToString());
                    await userManager.AddToRoleAsync(defaultUser, Roles.Developer.ToString());
                    await userManager.AddToRoleAsync(defaultUser, Roles.Maintainer.ToString());
                }
                await roleManager.SeedClaimForAdministration();
            }

        }
        private async static Task SeedClaimForAdministration(this RoleManager<IdentityRole> roleManager)
        {
            var adminRole = await roleManager.FindByNameAsync("Administration");
            await roleManager.AddPermissionClaim(adminRole, "Products");

        }
        public static async Task AddPermissionClaim(this RoleManager<IdentityRole> roleManager, IdentityRole role, string module)
        {

            var allClaims = await roleManager.GetClaimsAsync(role);
            var allPermissions = Permissions.GeneratePermissionsForModule(module);
            foreach(var permission in allPermissions)
            {
                if(!allClaims.Any(a=>a.Type=="Permission" && a.Value==permission))
                {
                    await roleManager.AddClaimAsync(role,new Claim("Permission",permission));
                }
            }
        }
    }
}

