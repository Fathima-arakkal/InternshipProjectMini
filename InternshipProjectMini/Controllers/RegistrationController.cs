using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using InternshipProjectMini.Models;
using InternshipProjectMini.Constants;
using Newtonsoft.Json;

public class RegistrationController : Controller
{
    private readonly UserManager<ApplicationUser> _userManager;
    private readonly RoleManager<IdentityRole> _roleManager;
    private readonly SignInManager<ApplicationUser> _signInManager;

    public RegistrationController(
        UserManager<ApplicationUser> userManager,
        RoleManager<IdentityRole> roleManager,
        SignInManager<ApplicationUser> signInManager)
    {
        _userManager = userManager;
        _roleManager = roleManager;
        _signInManager = signInManager;
    }

    [HttpGet]
    public IActionResult Register()
    {
        return RedirectToAction("Register", "Account"); // Redirect to AccountController's Register action
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Register(RegisterViewModel model)
    {
        if (ModelState.IsValid)
        {
            var user = new ApplicationUser
            {
                UserName = model.Email,
                Email = model.Email,
                SelectedPermissions = SerializePermissions(new PermissionViewModel())
            };

            var result = await _userManager.CreateAsync(user, model.Password);

            if (result.Succeeded)
            {
                // Check if "Basic" role exists and create if not
                var basicRoleExists = await _roleManager.RoleExistsAsync(Roles.Basic.ToString());
                if (!basicRoleExists)
                {
                    await _roleManager.CreateAsync(new IdentityRole(Roles.Basic.ToString()));
                }

                // Assign the "Basic" role to the user
                await _userManager.AddToRoleAsync(user, Roles.Basic.ToString());

                // Check if "ADMIN" role exists and create if not
                var adminRoleExists = await _roleManager.RoleExistsAsync(Roles.Admin.ToString());
                if (!adminRoleExists)
                {
                    await _roleManager.CreateAsync(new IdentityRole(Roles.Admin.ToString()));
                }

                // Assign the "ADMIN" role to the user
                await _userManager.AddToRoleAsync(user, Roles.Admin.ToString());

                // Check if "SuperAdmin" role exists and create if not
                var superAdminRoleExists = await _roleManager.RoleExistsAsync(Roles.SuperAdmin.ToString());
                if (!superAdminRoleExists)
                {
                    await _roleManager.CreateAsync(new IdentityRole(Roles.SuperAdmin.ToString()));
                }

                // Assign the "SuperAdmin" role to the user
                await _userManager.AddToRoleAsync(user, Roles.SuperAdmin.ToString());

                // Sign in the user after registration
                await _signInManager.SignInAsync(user, isPersistent: false);

                return RedirectToAction("Index", "Home");
            }

            foreach (var error in result.Errors)
            {
                ModelState.AddModelError(string.Empty, error.Description);
            }
        }

        // If registration fails, return to the registration page
        return View(model);
    }

    private string SerializePermissions(PermissionViewModel model)
    {
        return JsonConvert.SerializeObject(model);
    }


}

