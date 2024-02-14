using Microsoft.AspNetCore.Identity;
using InternshipProjectMini.Constants;
using System.Threading.Tasks;
using InternshipProjectMini.Models;

namespace InternshipProjectMini.Seeds
{
    public class DefaultRoles
    {
        public static async Task SeedAsync(UserManager<IdentityUser> userManager, RoleManager<IdentityRole> roleManager)
        {
            await roleManager.CreateAsync(new IdentityRole(Roles.SuperAdmin.ToString()));
            await roleManager.CreateAsync(new IdentityRole(Roles.Admin.ToString()));
            await roleManager.CreateAsync(new IdentityRole(Roles.Basic.ToString()));
        }

        internal static Task SeedAsync(UserManager<ApplicationUser> userManager, RoleManager<IdentityRole> roleManager)
        {
            throw new NotImplementedException();
        }
    }
}
