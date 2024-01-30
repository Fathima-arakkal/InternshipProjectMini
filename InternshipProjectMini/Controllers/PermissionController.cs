using InternshipProjectMini.Context;
using InternshipProjectMini.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Microsoft.AspNetCore.Identity;
using InternshipProjectMini.Models;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
namespace InternshipProjectMini.Controllers
{
    [Authorize]
    public class PermissionController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<ApplicationUser> _userManager;

        public PermissionController(ApplicationDbContext context, UserManager<ApplicationUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        public async Task<IActionResult> ManagePermissions()
        {
            // Fetch all modules (Department, Location, Employee, Machine)
            var modules = _context.Modules.ToList();

            // Fetch user permissions
            var user = await _userManager.GetUserAsync(User);
            var userPermissions = _context.UserPermissions.FirstOrDefault(p => p.UserId == user.Id);

            // Create ModuleViewModel list based on user's allowed modules
            var moduleViewModels = modules.Select(module => new ModuleViewModel
            {
                Id = module.Id,
                Name = module.Name,
                IsSelected = userPermissions?.AllowedModulesJson.Contains(module.Id.ToString()) ?? false
            }).ToList();

            return View(moduleViewModels);
        }

        [HttpPost]
        public async Task<IActionResult> ManagePermissions(List<ModuleViewModel> moduleViewModels)
        {
            // Save selected modules to user permissions
            var user = await _userManager.GetUserAsync(User);
            var userPermissions = _context.UserPermissions.FirstOrDefault(p => p.UserId == user.Id) ?? new UserPermission { UserId = user.Id };

            userPermissions.AllowedModulesJson = JsonConvert.SerializeObject(
                moduleViewModels.Where(m => m.IsSelected).Select(m => m.Id)
            );

            if (userPermissions.Id == 0)
            {
                _context.UserPermissions.Add(userPermissions);
            }
            else
            {
                _context.UserPermissions.Update(userPermissions);
            }

            _context.SaveChanges();

            return RedirectToAction("Index"); // Redirect to appropriate action
        }
    }
}