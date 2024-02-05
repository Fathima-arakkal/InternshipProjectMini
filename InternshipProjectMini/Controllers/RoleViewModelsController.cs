using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using InternshipProjectMini.Context;
using InternshipProjectMini.Models;

namespace InternshipProjectMini.Controllers
{
    public class RoleViewModelsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public RoleViewModelsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: RoleViewModels
        public async Task<IActionResult> Index()
        {
            return View(await _context.RoleViewModel.ToListAsync());
        }

        // GET: RoleViewModels/Details/5
        public async Task<IActionResult> Details(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var roleViewModel = await _context.RoleViewModel
                .FirstOrDefaultAsync(m => m.RoleId == id);
            if (roleViewModel == null)
            {
                return NotFound();
            }

            return View(roleViewModel);
        }

        // GET: RoleViewModels/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: RoleViewModels/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("RoleId,RoleName,Employee,Location,Machine,Department")] RoleViewModel roleViewModel)
        {
            if (ModelState.IsValid)
            {
                _context.Add(roleViewModel);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(roleViewModel);
        }

        // GET: RoleViewModels/Edit/5
        public async Task<IActionResult> Edit(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var roleViewModel = await _context.RoleViewModel.FindAsync(id);
            if (roleViewModel == null)
            {
                return NotFound();
            }
            return View(roleViewModel);
        }

        // POST: RoleViewModels/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(string id, [Bind("RoleId,RoleName,Employee,Location,Machine,Department")] RoleViewModel roleViewModel)
        {
            if (id != roleViewModel.RoleId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(roleViewModel);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!RoleViewModelExists(roleViewModel.RoleId))
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
            return View(roleViewModel);
        }

        // GET: RoleViewModels/Delete/5
        public async Task<IActionResult> Delete(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var roleViewModel = await _context.RoleViewModel
                .FirstOrDefaultAsync(m => m.RoleId == id);
            if (roleViewModel == null)
            {
                return NotFound();
            }

            return View(roleViewModel);
        }

        // POST: RoleViewModels/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(string id)
        {
            var roleViewModel = await _context.RoleViewModel.FindAsync(id);
            if (roleViewModel != null)
            {
                _context.RoleViewModel.Remove(roleViewModel);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool RoleViewModelExists(string id)
        {
            return _context.RoleViewModel.Any(e => e.RoleId == id);
        }
    }
}
