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
    public class UsersController : Controller
    {
        private readonly User_Context _context;

        public UsersController(User_Context context)
        {
            _context = context;
        }

        // GET: Users
        public async Task<IActionResult> Index()
        {
            return View(await _context.Users.ToListAsync());
        }



        // POST: Users/Login
        // POST: Users/Login
        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Login(string Email, string Password, Users users)
        {

            var test = _context.Users.Where(a => a.Email.Equals(Email) && a.Password.Equals(Password)).Count();

            if (test > 0)
            {
                //code attribution
                //this method was taken from benjii.me
                //https://benjii.me/2016/07/using-sessions-and-httpcontext-in-aspnetcore-and-mvc-core/
                //Ben Cull
                //https://bencull.com/
                //(Cull, 2016)
                HttpContext.Session.SetString("LoggedIn", "Yes");

                ViewBag.Error = "Success, explore Disaster Alleviation ";

                //code attribution
                //this method was taken DotNetTricks
                //https://www.dotnettricks.com/learn/mvc/return-view-vs-return-redirecttoaction-vs-return-redirect-vs-return-redirecttoroute
                //Shailendra Chauhan
                //https://www.dotnettricks.com/mentors/shailendra-chauhan
                //(Chauhan, 2022)
                return Redirect("/Home/Index");
            }

            else
            {
                HttpContext.Session.SetString("LoggedIn", "No");
                ViewBag.Error = "Invalid Credentials.";
                return View();
            }

        }

        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public IActionResult Login(string Email, string Password)
        //{
        //    var test = _context.Users.Where(a => a.Email.Equals(Email) && a.Password.Equals(Password)).Count();
        //    ViewBag.Message = test;

        //    if (test > 0)
        //    {
        //        ViewBag.Error = "Success, explore Disaster Alleviation ";
        //        return RedirectToAction("/Home/Index");
        //    }

        //    else
        //    {
        //        ViewBag.Message = "Wrong Password.";
        //        return View();
        //    }

        //}

        // GET: Users/Details/5
        public async Task<IActionResult> Details(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var users = await _context.Users
                .FirstOrDefaultAsync(m => m.Email == id);
            if (users == null)
            {
                return NotFound();
            }

            return View(users);
        }

        // GET: Users/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Users/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Email,Username,Password")] Users users)
        {
            if (ModelState.IsValid)
            {
                _context.Add(users);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(users);
        }

        // GET: Users/Edit/5
        public async Task<IActionResult> Edit(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var users = await _context.Users.FindAsync(id);
            if (users == null)
            {
                return NotFound();
            }
            return View(users);
        }

        // POST: Users/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(string id, [Bind("Email,Username,Password")] Users users)
        {
            if (id != users.Email)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(users);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!UsersExists(users.Email))
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
            return View(users);
        }

        // GET: Users/Delete/5
        public async Task<IActionResult> Delete(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var users = await _context.Users
                .FirstOrDefaultAsync(m => m.Email == id);
            if (users == null)
            {
                return NotFound();
            }

            return View(users);
        }

        // POST: Users/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(string id)
        {
            var users = await _context.Users.FindAsync(id);
            _context.Users.Remove(users);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool UsersExists(string id)
        {
            return _context.Users.Any(e => e.Email == id);
        }
    }
}
