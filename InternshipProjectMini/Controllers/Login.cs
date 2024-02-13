using InternshipProjectMini.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace InternshipProjectMini.Controllers
{
    public class Login : Controller
    {
        public class AccountController : Controller
        {
            private readonly SignInManager<ApplicationUser> _signInManager;

            public AccountController(SignInManager<ApplicationUser> signInManager)
            {
                _signInManager = signInManager;
            }

            [HttpGet]
            public IActionResult Login()
            {
                return View();
            }

            [HttpPost]
            public async Task<IActionResult> Login(LoginViewModel model)
            {
                if (ModelState.IsValid)
                {
                    var result = await _signInManager.PasswordSignInAsync(model.Username, model.Password, model.RememberMe, lockoutOnFailure: false);

                    if (result.Succeeded)
                    {
                        return RedirectToAction("Index", "Home");
                    }

                    ModelState.AddModelError(string.Empty, "Invalid login attempt.");
                }

                return View(model);
            }
        }
    }
}
