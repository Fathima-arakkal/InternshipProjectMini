using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace InternshipProjectMini.Controllers
{
    [Authorize(Roles = "SuperAdmin")]
    public class AdminController : Controller
    {
        private readonly UserManager<IdentityUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;

        public AdminController(UserManager<IdentityUser> userManager, RoleManager<IdentityRole> roleManager)
        {
            _userManager = userManager;
            _roleManager = roleManager;
        }

        public async Task<IActionResult> AssignSuperAdminRole()
        {
            var user = await _userManager.FindByEmailAsync("superadmin@gmail.com");

            if (user != null)
            {
                if (!await _roleManager.RoleExistsAsync("SuperAdmin"))
                {
                    await _roleManager.CreateAsync(new IdentityRole("SuperAdmin"));
                }

                await _userManager.AddToRoleAsync(user, "SuperAdmin");

                return RedirectToAction("Index", "Home");
            }
            else
            {
                return NotFound();
            }
        }
    }
}
