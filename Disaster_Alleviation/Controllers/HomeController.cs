﻿using Disaster_Alleviation.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace Disaster_Alleviation.Controllers
{
    public class HomeController : Controller
    {
        private readonly Monetary_donations_Context _Mcontext;
        private readonly Purchase_Context _Pcontext;
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger, Monetary_donations_Context Mcontext, Purchase_Context  Pcontext)
        {
            _logger = logger;
            _Mcontext = Mcontext;
            _Pcontext = Pcontext;


        }

        public IActionResult Index()
        {
            var purchaseTotal = _Pcontext.Purchase.Where(x => x.Amount >= 0).Sum(y => y.Amount);
            ViewBag.purchaseTotal = purchaseTotal;

            var total = _Mcontext.Monetary_donations.Where(x => x.Amount >= 0).Sum(y => y.Amount);
            ViewBag.Total = total;
            ViewBag.Left = total - purchaseTotal;

            HttpContext.Session.SetString("Test", "Ben Rules");
            ViewBag.Name = "Jess";
            ViewBag.Date = DateTime.Now;
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
