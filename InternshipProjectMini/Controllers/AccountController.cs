using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using InternshipProjectMini.Models;
using Newtonsoft.Json;
using InternshipProjectMini.Constants;
using Microsoft.AspNetCore.Authorization;

namespace InternshipProjectMini.Controllers
{
    public class AccountController : Controller
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly RoleManager<IdentityRole> _roleManager;

        public AccountController(
            UserManager<ApplicationUser> userManager,
            SignInManager<ApplicationUser> signInManager,
            RoleManager<IdentityRole> roleManager)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _roleManager = roleManager;
        }

        [HttpGet]
        public IActionResult Register()
        {
            return View();
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
                    // Assign roles to the user
                    await _userManager.AddToRoleAsync(user, Roles.Basic.ToString());
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

            return View(model);
        }


        [HttpGet]
        public async Task<IActionResult> Login()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login(LoginViewModel model, string returnUrl = null)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            var result = await _signInManager.PasswordSignInAsync(model.Email, model.Password, model.RememberMe, lockoutOnFailure: false);

            if (result.Succeeded)
            {
                if (!string.IsNullOrEmpty(returnUrl) && Url.IsLocalUrl(returnUrl))
                {
                    return Redirect(returnUrl);
                }
                else
                {
                    return RedirectToAction("Index", "Home");
                }
            }

            ModelState.AddModelError(string.Empty, "Invalid login attempt.");

            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Logout()
        {
            await _signInManager.SignOutAsync();
            return RedirectToAction("Index", "Home");
        }

        [HttpGet]
        public async Task<IActionResult> ManagePermissions()
        {
            var userPermissions = await GetCurrentUserPermissions();
            var model = new PermissionViewModel
            {
                Employee = userPermissions.Employee,
                Department = userPermissions.Department,
                Machine = userPermissions.Machine,
                Location = userPermissions.Location
            };

            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> ManagePermissions(PermissionViewModel model)
        {
            await SaveUserPermissions(model);
            return RedirectToAction("Index", "Home", new { permissions = SerializePermissions(model) });
        }

        private async Task<PermissionViewModel> GetCurrentUserPermissions()
        {
            var user = await GetUserAsync();

            if (user == null)
            {
                return new PermissionViewModel();
            }

            var permissions = DeserializePermissions(user.SelectedPermissions);

            return permissions;
        }

        private async Task SaveUserPermissions(PermissionViewModel model)
        {
            var user = await GetUserAsync();

            user.SelectedPermissions = SerializePermissions(model);

            await _userManager.UpdateAsync(user);
        }

        private PermissionViewModel DeserializePermissions(string permissions)
        {
            return JsonConvert.DeserializeObject<PermissionViewModel>(permissions);
        }

        private string SerializePermissions(PermissionViewModel model)
        {
            return JsonConvert.SerializeObject(model);
        }

        private async Task<ApplicationUser> GetUserAsync()
        {
            return await _userManager.GetUserAsync(User);
        }

        private string SerializeStaticPermissions(PermissionViewModel model)
        {
            var permissions = new
            {
                Employee = model.Employee,
                Department = model.Department,
                Machine = model.Machine,
                Location = model.Location
            };

            return JsonConvert.SerializeObject(permissions);
        }
        [HttpGet]
        [AllowAnonymous]
        public IActionResult AccessDenied()
        {
            return View();
        }

    }
}
