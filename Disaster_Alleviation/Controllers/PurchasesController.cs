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
    public class PurchasesController : Controller
    {
        private readonly Purchase_Context _context;
        private readonly Monetary_donations_Context _Mcontext;

        public PurchasesController(Purchase_Context context, Monetary_donations_Context Mcontext)
        {
            _context = context;
            _Mcontext = Mcontext;
        }
        //code attribution
        //this method was taken from benjii.me
        //https://stackoverflow.com/questions/2978736/linq-and-conditional-sum
        //(StackOverflow, 2010)
        // GET: Purchases
        public async Task<IActionResult> Index()
        {
            if (HttpContext.Session.GetString("LoggedIn") != "Yes")
            {
                return Redirect("/Users/Login");
            }
            var purchaseTotal = _context.Purchase.Where(x => x.Amount >= 0).Sum(y => y.Amount);
            ViewBag.purchaseTotal = purchaseTotal;

            var total = _Mcontext.Monetary_donations.Where(x => x.Amount >= 0).Sum(y => y.Amount);
            ViewBag.Total = total;
            ViewBag.Left = total - purchaseTotal;
            if(purchaseTotal >= total)
            {
                ViewBag.Warning = "WARNING - you don't have enough funds to purchase goods";
            }
            return View(await _context.Purchase.ToListAsync());
        }

        // GET: Purchases/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var purchase = await _context.Purchase
                .FirstOrDefaultAsync(m => m.PurchaseID == id);
            if (purchase == null)
            {
                return NotFound();
            }

            return View(purchase);
        }

        // GET: Purchases/Create
        public IActionResult Create()
        {
            ViewBag.DisasterID = HttpContext.Session.GetString("DisasterID");
            ViewBag.DisasterName = HttpContext.Session.GetString("DisasterName");
            ViewBag.Location = HttpContext.Session.GetString("Location");
            var list = _context.Purchase.Select(x => x.Goods_Category).Distinct().ToList();
            ViewData["list"] = list;
            return View();
        }

        // POST: Purchases/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("PurchaseID,DisasterID, DisasterName, Location,Goods_Category,Num_items,Goods_Description,PurchaseDate,Amount")] Purchase purchase)
        {
            if (ModelState.IsValid)
            {
                string disasterID = HttpContext.Session.GetString("DisasterID");
                purchase.DisasterID = Int32.Parse(disasterID);
                string disasterName = HttpContext.Session.GetString("DisasterName");
                purchase.DisasterName = disasterName;
                string location = HttpContext.Session.GetString("Location");
                purchase.Location = location;

                _context.Add(purchase);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(purchase);
        }

        // GET: Purchases/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var purchase = await _context.Purchase.FindAsync(id);
            if (purchase == null)
            {
                return NotFound();
            }
            return View(purchase);
        }

        // POST: Purchases/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("PurchaseID,Goods_Category,Num_items,Goods_Description,PurchaseDate,Amount")] Purchase purchase)
        {
            if (id != purchase.PurchaseID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(purchase);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!PurchaseExists(purchase.PurchaseID))
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
            return View(purchase);
        }
        // GET: Purchases/Allocate
        public async Task<IActionResult> AllocatePurchases(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var purchase = await _context.Purchase.FindAsync(id);
            if (purchase == null)
            {
                return NotFound();
            }
            return View(purchase);
        }

        // POST: Purchases/Allocate
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> AllocatePurchases(int id, [Bind("PurchaseID, DisasterID, DisasterName, Location, Goods_Category,Num_items,Goods_Description,PurchaseDate,Amount")]  Purchase purchase)
        {
            if (id != purchase.PurchaseID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    string disasterID = HttpContext.Session.GetString("DisasterID");
                    purchase.DisasterID = Int32.Parse(disasterID);
                    string disasterName = HttpContext.Session.GetString("DisasterName");
                    purchase.DisasterName = disasterName;
                    string location = HttpContext.Session.GetString("Location");
                    purchase.Location = location;

                    _context.Update(purchase);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!PurchaseExists(purchase.PurchaseID))
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
            return View(purchase);
        }

        // GET: Purchases/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var purchase = await _context.Purchase
                .FirstOrDefaultAsync(m => m.PurchaseID == id);
            if (purchase == null)
            {
                return NotFound();
            }

            return View(purchase);
        }

        // POST: Purchases/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var purchase = await _context.Purchase.FindAsync(id);
            _context.Purchase.Remove(purchase);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool PurchaseExists(int id)
        {
            return _context.Purchase.Any(e => e.PurchaseID == id);
        }
    }
}
