using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Disaster_Alleviation.Models;
using Microsoft.AspNetCore.Http;

namespace Disaster_Alleviation.Controllers
{
    public class Monetary_donationsController : Controller
    {
        private readonly Monetary_donations_Context _context;

        public Monetary_donationsController(Monetary_donations_Context context)
        {
            _context = context;
        }

        // GET: Monetary_donations
        public async Task<IActionResult> Index()
        {
            if (HttpContext.Session.GetString("LoggedIn") != "Yes")
            {
                return Redirect("/Users/Login");
            }
            var total = _context.Monetary_donations.Where(x => x.Amount >= 0).Sum(y => y.Amount);
            ViewBag.Total = total;
            return View(await _context.Monetary_donations.ToListAsync());
            //var total= _context.Monetary_donations.Sum(x => x.Amount);
            //ViewData["total"] = total;
        }

        // GET: Monetary_donations/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            
            if (id == null)
            {
                return NotFound();
            }

            var monetary_donations = await _context.Monetary_donations
                .FirstOrDefaultAsync(m => m.MonetaryID == id);
            if (monetary_donations == null)
            {
                return NotFound();
            }

            return View(monetary_donations);
        }

        // GET: Monetary_donations/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Monetary_donations/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("MonetaryID,Amount,DonationDate,Donor")] Monetary_donations monetary_donations, string Donor)
        {
            if (ModelState.IsValid)
            {
                if (Donor == null)
                {
                    monetary_donations.Donor = "Anonymous";
                }
                _context.Add(monetary_donations);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(monetary_donations);
        }

        // GET: Monetary_donations/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var monetary_donations = await _context.Monetary_donations.FindAsync(id);
            if (monetary_donations == null)
            {
                return NotFound();
            }
            return View(monetary_donations);
        }

        // POST: Monetary_donations/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("MonetaryID,Amount,DonationDate,Donor")] Monetary_donations monetary_donations)
        {
            if (id != monetary_donations.MonetaryID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(monetary_donations);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!Monetary_donationsExists(monetary_donations.MonetaryID))
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
            return View(monetary_donations);
        }

        // GET: Monetary_donations/Edit/5
        public async Task<IActionResult> AllocateMoney(int? id)
        {
            ViewBag.DisasterID = HttpContext.Session.GetString("DisasterID");
            ViewBag.DisasterName = HttpContext.Session.GetString("DisasterName");
            ViewBag.Location = HttpContext.Session.GetString("Location");
            HttpContext.Session.SetString("GoodsID", id.ToString());

            if (id == null)
            {
                return NotFound();
            }

            var monetary_donations = await _context.Monetary_donations.FindAsync(id);
            if (monetary_donations == null)
            {
                return NotFound();
            }
            return View(monetary_donations);
        }

        // POST: Monetary_donations/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> AllocateMoney(int id, [Bind("AllocateM, DisasterID, DisasterName, Location,Amount,DonationDate,Donor")] AllocateMoney allocateMoney, Monetary_donations monetary_donations)
        {

            if (ModelState.IsValid)
            {
                try
                {
                    string disasterID = HttpContext.Session.GetString("DisasterID");
                    allocateMoney.DisasterID = Int32.Parse(disasterID);
                    string disasterName = HttpContext.Session.GetString("DisasterName");
                    allocateMoney.DisasterName = disasterName;
                    string location = HttpContext.Session.GetString("Location");
                    allocateMoney.Location = location;

                    _context.Add(allocateMoney);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!Monetary_donationsExists(monetary_donations.MonetaryID))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }



                int gID = Int32.Parse(HttpContext.Session.GetString("GoodsID"));

                var Goods_donations = await _context.Monetary_donations
                 .FirstOrDefaultAsync(m => m.MonetaryID == gID);
                _context.Monetary_donations.Remove(Goods_donations);
                await _context.SaveChangesAsync();
                return Redirect("/AllocateMoneys/Index");
            }
            return View(allocateMoney);
        }
        // GET: Monetary_donations/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var monetary_donations = await _context.Monetary_donations
                .FirstOrDefaultAsync(m => m.MonetaryID == id);
            if (monetary_donations == null)
            {
                return NotFound();
            }

            return View(monetary_donations);
        }

        // POST: Monetary_donations/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var monetary_donations = await _context.Monetary_donations.FindAsync(id);
            _context.Monetary_donations.Remove(monetary_donations);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool Monetary_donationsExists(int id)
        {
            return _context.Monetary_donations.Any(e => e.MonetaryID == id);
        }
    }
}
