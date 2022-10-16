using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Disaster_Alleviation.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;

namespace Disaster_Alleviation.Controllers
{
    public class Goods_donationsController : Controller
    {
        private readonly Goods_donation_Context _context;

        public Goods_donationsController(Goods_donation_Context context)
        {
            _context = context;
        }

        // GET: Goods_donations
  
        public async Task<IActionResult> Index()
        {
            if (HttpContext.Session.GetString("LoggedIn") != "Yes")
            {
                return Redirect("/Users/Login");
            }

            return View(await _context.Goods_donations.ToListAsync());
        }

        // GET: Goods_donations/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var goods_donations = await _context.Goods_donations
                .FirstOrDefaultAsync(m => m.GoodsID == id);
            if (goods_donations == null)
            {
                return NotFound();
            }

            return View(goods_donations);
        }

        // GET: Goods_donations/Create
        public IActionResult Create()
        {
            var list = _context.Goods_donations.Select(x => x.Goods_Category).Distinct().ToList();
            ViewData["list"] = list;
            //ViewBag.Category = HttpContext.Session.GetString("Categories");
            return View();
        }
         

        // POST: Goods_donations/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("GoodsID,Goods_Category,Num_items,Goods_Description,DonationDate,Goods_Donor")] Goods_donations goods_donations, string Goods_Donor)
        {
            if (ModelState.IsValid)
            {

                if (Goods_Donor == null)
                {
                    goods_donations.Goods_Donor = "Anonymous";
                }

                //string category = HttpContext.Session.GetString("Goods_Category");
                //goods_donations.Goods_Category = category;


                _context.Add(goods_donations);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(goods_donations);
        }

        // GET: Goods_donations/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var goods_donations = await _context.Goods_donations.FindAsync(id);
            if (goods_donations == null)
            {
                return NotFound();
            }
            return View(goods_donations);
        }

        // POST: Goods_donations/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("GoodsID,Goods_Category,Num_items,Goods_Description,DonationDate,Goods_Donor")] Goods_donations goods_donations)
        {
            if (id != goods_donations.GoodsID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(goods_donations);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!Goods_donationsExists(goods_donations.GoodsID))
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
            return View(goods_donations);
        }
        // GET: Goods_donations/Allocate Goods
        public async Task<IActionResult> AllocateGoods(int? id)
        {
            ViewBag.DisasterID = HttpContext.Session.GetString("DisasterID");
            ViewBag.DisasterName = HttpContext.Session.GetString("DisasterName");
            ViewBag.Location= HttpContext.Session.GetString("Location");
            HttpContext.Session.SetString("GoodsID", id.ToString());
            
            if (id == null)
            {
                return NotFound();
            }

            var goods_donations = await _context.Goods_donations.FindAsync(id);
            if (goods_donations == null)
            {
                return NotFound();
            }
            return View(goods_donations);
        }

        // POST: Goods_donations/Allocate Goods
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> AllocateGoods(int id, [Bind("AllocateG,DisasterID,DisasterName, Location, Goods_Category,Num_items,Goods_Description,DonationDate,Goods_Donor")] AllocateGoods allocateGoods, Goods_donations goods_donations)
        {
          

            if (ModelState.IsValid)
            {
                try
                {
                    string disasterID = HttpContext.Session.GetString("DisasterID");
                    allocateGoods.DisasterID = Int32.Parse(disasterID);
                    string disasterName = HttpContext.Session.GetString("DisasterName");
                    allocateGoods.DisasterName = disasterName;
                    string location = HttpContext.Session.GetString("Location");
                    allocateGoods.Location = location;

                    _context.Add(allocateGoods);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!Goods_donationsExists(goods_donations.GoodsID))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }

                int gID = Int32.Parse(HttpContext.Session.GetString("GoodsID"));

                var Goods_donations = await _context.Goods_donations
                 .FirstOrDefaultAsync(m => m.GoodsID == gID);
                _context.Goods_donations.Remove(Goods_donations);
                await _context.SaveChangesAsync();
                return Redirect("/AllocateGoods/Index");
            }
            return View(allocateGoods);
        }
            
        

        // GET: Goods_donations/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var goods_donations = await _context.Goods_donations
                .FirstOrDefaultAsync(m => m.GoodsID == id);
            if (goods_donations == null)
            {
                return NotFound();
            }

            return View(goods_donations);
        }

        // POST: Goods_donations/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var goods_donations = await _context.Goods_donations.FindAsync(id);
            _context.Goods_donations.Remove(goods_donations);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool Goods_donationsExists(int id)
        {
            return _context.Goods_donations.Any(e => e.GoodsID == id);
        }
    }
}
