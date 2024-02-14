using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using InternshipProjectMini.Models;
using InternshipProjectMini.Constants;

public class RegistrationController : Controller
{
    private readonly UserManager<ApplicationUser> _userManager;

    public RegistrationController(UserManager<ApplicationUser> userManager)
    {
        _userManager = userManager;
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
        // This method will not be called as it redirects to the Register action in AccountController
        // You can add specific logic here if needed.
        return RedirectToAction("Index", "Home");
    }
}

