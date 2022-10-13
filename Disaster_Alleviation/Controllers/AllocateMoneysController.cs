using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Disaster_Alleviation.Models;

namespace Disaster_Alleviation.Controllers
{
    public class AllocateMoneysController : Controller
    {
        private readonly Monetary_donations_Context _context;

        public AllocateMoneysController(Monetary_donations_Context context)
        {
            _context = context;
        }

        // GET: AllocateMoneys
        public async Task<IActionResult> Index()
        {
            return View(await _context.AllocateMoney.ToListAsync());
        }

        // GET: AllocateMoneys/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var allocateMoney = await _context.AllocateMoney
                .FirstOrDefaultAsync(m => m.MonetaryID == id);
            if (allocateMoney == null)
            {
                return NotFound();
            }

            return View(allocateMoney);
        }

        // GET: AllocateMoneys/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: AllocateMoneys/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("MonetaryID,Amount,DonationDate,Donor")] AllocateMoney allocateMoney)
        {
            if (ModelState.IsValid)
            {
                _context.Add(allocateMoney);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(allocateMoney);
        }

        // GET: AllocateMoneys/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var allocateMoney = await _context.AllocateMoney.FindAsync(id);
            if (allocateMoney == null)
            {
                return NotFound();
            }
            return View(allocateMoney);
        }

        // POST: AllocateMoneys/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("MonetaryID,Amount,DonationDate,Donor")] AllocateMoney allocateMoney)
        {
            if (id != allocateMoney.MonetaryID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(allocateMoney);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!AllocateMoneyExists(allocateMoney.MonetaryID))
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
            return View(allocateMoney);
        }

        // GET: AllocateMoneys/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var allocateMoney = await _context.AllocateMoney
                .FirstOrDefaultAsync(m => m.MonetaryID == id);
            if (allocateMoney == null)
            {
                return NotFound();
            }

            return View(allocateMoney);
        }

        // POST: AllocateMoneys/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var allocateMoney = await _context.AllocateMoney.FindAsync(id);
            _context.AllocateMoney.Remove(allocateMoney);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool AllocateMoneyExists(int id)
        {
            return _context.AllocateMoney.Any(e => e.MonetaryID == id);
        }
    }
}
