using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using NutriApp.Data;
using NutriApp.Models;

namespace NutriApp.Controllers
{
    [Authorize]
    public class AlimentacaoController : Controller
    {
        private readonly ApplicationDbContext _context;

        public AlimentacaoController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Alimentacao
        public async Task<IActionResult> Index()
        {
            return _context.alimentacaoDias != null ?
                        View(await _context.alimentacaoDias.Where(x => x.UserName == User.Identity.Name).ToListAsync()) :
                        Problem("Entity set 'ApplicationDbContext.alimentacaoDias'  is null.");
        }

        // GET: Alimentacao/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.alimentacaoDias == null)
            {
                return NotFound();
            }

            var alimentacaoDia = await _context.alimentacaoDias
                .FirstOrDefaultAsync(m => m.Id == id);
            if (alimentacaoDia == null)
            {
                return NotFound();
            }

            return View(alimentacaoDia);
        }

        // GET: Alimentacao/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Alimentacao/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,TotalCalorias,Dia,DiaDate")] AlimentacaoDia alimentacaoDia)
        {
            alimentacaoDia.UserName = User.Identity.Name;
            if (ModelState.IsValid)
            {
                _context.Add(alimentacaoDia);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(alimentacaoDia);
        }

        // GET: Alimentacao/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.alimentacaoDias == null)
            {
                return NotFound();
            }

            var alimentacaoDia = await _context.alimentacaoDias.FindAsync(id);
            if (alimentacaoDia == null)
            {
                return NotFound();
            }
            return View(alimentacaoDia);
        }

        // POST: Alimentacao/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,TotalCalorias,Dia,DiaDate")] AlimentacaoDia alimentacaoDia)
        {
            if (id != alimentacaoDia.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(alimentacaoDia);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!AlimentacaoDiaExists(alimentacaoDia.Id))
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
            return View(alimentacaoDia);
        }

        // GET: Alimentacao/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.alimentacaoDias == null)
            {
                return NotFound();
            }

            var alimentacaoDia = await _context.alimentacaoDias
                .FirstOrDefaultAsync(m => m.Id == id);
            if (alimentacaoDia == null)
            {
                return NotFound();
            }

            return View(alimentacaoDia);
        }

        // POST: Alimentacao/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.alimentacaoDias == null)
            {
                return Problem("Entity set 'ApplicationDbContext.alimentacaoDias'  is null.");
            }
            var alimentacaoDia = await _context.alimentacaoDias.FindAsync(id);
            if (alimentacaoDia != null)
            {
                _context.alimentacaoDias.Remove(alimentacaoDia);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool AlimentacaoDiaExists(int id)
        {
            return (_context.alimentacaoDias?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
