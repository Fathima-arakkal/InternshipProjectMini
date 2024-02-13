using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using InternshipProjectMini.Constants;
using InternshipProjectMini.Helpers;
using InternshipProjectMini.Models;
using InternshipProjectMini.Seeds;
using System.Threading.Tasks;
using System.Collections.Generic;
using System;
using System.Linq;

namespace InternshipProjectMini.Controllers
{
    [Authorize(Roles ="Administration")]
    public class PermissionController : Controller
    {
        private readonly RoleManager<IdentityRole> _roleManager;

        public PermissionController(RoleManager<IdentityRole> roleManager)
        {
            _roleManager = roleManager;

        }
        [HttpGet]
        public async Task<IActionResult> Index (String roleId)

        {
            var model = new PermissionViewModel();
            var allPermissions = new List<RoleClaimsViewModel>();
            allPermissions.GetPermissions(typeof(Permissions.Products), roleId);
            var role =await _roleManager.FindByIdAsync(roleId);
            if (role == null)
            {
                // Handle the case where the role is not found
                return NotFound();
            }
            model.RoleId = roleId;
            var claims = await _roleManager.GetClaimsAsync(role);
            var allClaimValues = allPermissions.Select(a => a.Value).ToList();
            var roleClaimValues = claims.Select(a => a.Value).ToList();
            var authorizedClaims = allClaimValues.Intersect(roleClaimValues).ToList();
            foreach(var permission in allPermissions)
            {
                if (authorizedClaims.Any(a => a == permission.Value))
                {
                    permission.Selected = true;
                }
            }
            model.RoleClaims = allPermissions;
            return View(model);
        }
        [HttpPost]
        public async Task<IActionResult> Update (PermissionViewModel model)
        {
            var role = await _roleManager.FindByIdAsync (model.RoleId);
            var claims = await _roleManager.GetClaimsAsync(role);
            foreach (var claim in claims)
            {
                await _roleManager.RemoveClaimAsync(role, claim);
            }
            var selectedClaims = model.RoleClaims.Where(a => a.Selected).ToList();
            foreach (var claim in selectedClaims)
            {
                await InternshipProjectMini.Helpers.ClaimsHelper.AddPermissionClaim(_roleManager, role, claim.Value);

            }
            return RedirectToAction("Index",new { roleId = model.RoleId });
        }
    }
}
