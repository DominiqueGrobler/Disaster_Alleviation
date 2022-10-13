
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
    public class AllocateGoodsController : Controller
    {
        private readonly Goods_donation_Context _context;

        public AllocateGoodsController(Goods_donation_Context context)
        {
            _context = context;
        }

        // GET: AllocateGoods
        public async Task<IActionResult> Index()
        {
            return View(await _context.AllocateGoods.ToListAsync());
        }

        // GET: AllocateGoods/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var allocateGoods = await _context.AllocateGoods
                .FirstOrDefaultAsync(m => m.GoodsID == id);
            if (allocateGoods == null)
            {
                return NotFound();
            }

            return View(allocateGoods);
        }

        // GET: AllocateGoods/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: AllocateGoods/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("GoodsID,Goods_Category,Num_items,Goods_Description,DonationDate,Goods_Donor")] AllocateGoods allocateGoods)
        {
            if (ModelState.IsValid)
            {
                _context.Add(allocateGoods);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(allocateGoods);
        }

        // GET: AllocateGoods/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var allocateGoods = await _context.AllocateGoods.FindAsync(id);
            if (allocateGoods == null)
            {
                return NotFound();
            }
            return View(allocateGoods);
        }

        // POST: AllocateGoods/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("GoodsID,Goods_Category,Num_items,Goods_Description,DonationDate,Goods_Donor")] AllocateGoods allocateGoods)
        {
            if (id != allocateGoods.GoodsID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(allocateGoods);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!AllocateGoodsExists(allocateGoods.GoodsID))
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
            return View(allocateGoods);
        }

        // GET: AllocateGoods/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var allocateGoods = await _context.AllocateGoods
                .FirstOrDefaultAsync(m => m.GoodsID == id);
            if (allocateGoods == null)
            {
                return NotFound();
            }

            return View(allocateGoods);
        }

        // POST: AllocateGoods/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var allocateGoods = await _context.AllocateGoods.FindAsync(id);
            _context.AllocateGoods.Remove(allocateGoods);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool AllocateGoodsExists(int id)
        {
            return _context.AllocateGoods.Any(e => e.GoodsID == id);
        }
    }
}
