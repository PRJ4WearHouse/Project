using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using WearHouse_WebApp.Data;
using WearHouse_WebApp.Models;

namespace WearHouse_WebApp.Controllers
{
    public class WearablesController : Controller
    {
        private readonly ApplicationDbContext _context;

        public WearablesController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Wearables
        public async Task<IActionResult> Index()
        {
            return View(await _context.Wearables.ToListAsync());
        }

        // GET: Wearables/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var wearable = await _context.Wearables
                .FirstOrDefaultAsync(m => m.WearableId == id);
            if (wearable == null)
            {
                return NotFound();
            }

            return View(wearable);
        }

        // GET: Wearables/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Wearables/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("WearableId,Title,Description,ImageUrls")] Wearable wearable)
        {
            if (ModelState.IsValid)
            {
                _context.Add(wearable);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(wearable);
        }

        // GET: Wearables/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var wearable = await _context.Wearables.FindAsync(id);
            if (wearable == null)
            {
                return NotFound();
            }
            return View(wearable);
        }

        // POST: Wearables/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("WearableId,Title,Description,ImageUrls")] Wearable wearable)
        {
            if (id != wearable.WearableId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(wearable);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!WearableExists(wearable.WearableId))
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
            return View(wearable);
        }

        // GET: Wearables/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var wearable = await _context.Wearables
                .FirstOrDefaultAsync(m => m.WearableId == id);
            if (wearable == null)
            {
                return NotFound();
            }

            return View(wearable);
        }

        // POST: Wearables/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var wearable = await _context.Wearables.FindAsync(id);
            _context.Wearables.Remove(wearable);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool WearableExists(int id)
        {
            return _context.Wearables.Any(e => e.WearableId == id);
        }
    }
}
