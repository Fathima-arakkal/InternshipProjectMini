using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using InternshipProjectMini.Context;
using InternshipProjectMini.Models;

namespace InternshipProjectMini
{
    public class UserViewModelsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public UserViewModelsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: UserViewModels
        public async Task<IActionResult> Index()
        {
            var applicationDbContext = _context.UserViewModel.Include(u => u.Role);
            return View(await applicationDbContext.ToListAsync());
        }

        // GET: UserViewModels/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var userViewModel = await _context.UserViewModel
                .Include(u => u.Role)
                .FirstOrDefaultAsync(m => m.UserId == id);
            if (userViewModel == null)
            {
                return NotFound();
            }

            return View(userViewModel);
        }

        // GET: UserViewModels/Create
        public IActionResult Create()
        {
            ViewData["RoleId"] = new SelectList(_context.RoleViewModel, "RoleId", "RoleId");
            return View();
        }

        // POST: UserViewModels/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("UserId,Name,Email,PhoneNumber,Username,Password,RoleId,EmployeeAccess,LocationAccess,MachineAccess,DepartmentAccess")] UserViewModel userViewModel)
        {
            
                _context.Add(userViewModel);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            
            ViewData["RoleId"] = new SelectList(_context.RoleViewModel, "RoleId", "RoleId", userViewModel.RoleId);
            return View(userViewModel);
        }

        // GET: UserViewModels/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var userViewModel = await _context.UserViewModel.FindAsync(id);
            if (userViewModel == null)
            {
                return NotFound();
            }
            ViewData["RoleId"] = new SelectList(_context.RoleViewModel, "RoleId", "RoleId", userViewModel.RoleId);
            return View(userViewModel);
        }

        // POST: UserViewModels/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("UserId,Name,Email,PhoneNumber,Username,Password,RoleId,EmployeeAccess,LocationAccess,MachineAccess,DepartmentAccess")] UserViewModel userViewModel)
        {
            if (id != userViewModel.UserId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(userViewModel);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!UserViewModelExists(userViewModel.UserId))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            ViewData["RoleId"] = new SelectList(_context.RoleViewModel, "RoleId", "RoleId", userViewModel.RoleId);
            return View(userViewModel);
        }

        // GET: UserViewModels/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var userViewModel = await _context.UserViewModel
                .Include(u => u.Role)
                .FirstOrDefaultAsync(m => m.UserId == id);
            if (userViewModel == null)
            {
                return NotFound();
            }

            return View(userViewModel);
        }

        // POST: UserViewModels/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var userViewModel = await _context.UserViewModel.FindAsync(id);
            if (userViewModel != null)
            {
                _context.UserViewModel.Remove(userViewModel);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool UserViewModelExists(int id)
        {
            return _context.UserViewModel.Any(e => e.UserId == id);
        }
    }
}
