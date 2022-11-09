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
    public class DisastersController : Controller
    {
        private readonly Disaster_Context _context;

        public DisastersController(Disaster_Context context)
        {
            _context = context;
        }

        // GET: Disasters
        //code attribution
        //this method was taken DotNetTricks
        //https://www.dotnettricks.com/learn/mvc/return-view-vs-return-redirecttoaction-vs-return-redirect-vs-return-redirecttoroute
        //Shailendra Chauhan
        //https://www.dotnettricks.com/mentors/shailendra-chauhan
        //(Chauhan, 2022)
        public async Task<IActionResult> Index()
        {
            if (HttpContext.Session.GetString("LoggedIn") != "Yes")
            {
                return Redirect("/Users/Login");
            }


            return View(await _context.Disaster.ToListAsync());
        }

        // GET: Disasters/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            
            if (id == null)
            {
                return NotFound();
            }

            var disaster = await _context.Disaster
                .FirstOrDefaultAsync(m => m.DisasterID == id);
            if (disaster == null)
            {
                return NotFound();
            }


            HttpContext.Session.SetString("DisasterID", disaster.DisasterID.ToString());
            HttpContext.Session.SetString("DisasterName", disaster.DisasterName.ToString());
            HttpContext.Session.SetString("Location", disaster.Location.ToString());
            return View(disaster);
        }

        // GET: Disasters/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Disasters/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("DisasterID,DisasterName,Location,Description,StartDate,EndDate,AidType, Status")] Disaster disaster)
        {
            if (ModelState.IsValid)
            {
                _context.Add(disaster);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(disaster);

           
        }

        // GET: Disasters/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var disaster = await _context.Disaster.FindAsync(id);
            if (disaster == null)
            {
                return NotFound();
            }
            return View(disaster);
        }

        // POST: Disasters/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("DisasterID,DisasterName,Location,Description,StartDate,EndDate,AidType")] Disaster disaster)
        {
            if (id != disaster.DisasterID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(disaster);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!DisasterExists(disaster.DisasterID))
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
            return View(disaster);
        }

        // GET: Disasters/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var disaster = await _context.Disaster
                .FirstOrDefaultAsync(m => m.DisasterID == id);
            if (disaster == null)
            {
                return NotFound();
            }

            return View(disaster);
        }

        // POST: Disasters/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var disaster = await _context.Disaster.FindAsync(id);
            _context.Disaster.Remove(disaster);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool DisasterExists(int id)
        {
            return _context.Disaster.Any(e => e.DisasterID == id);
        }
    }
}
