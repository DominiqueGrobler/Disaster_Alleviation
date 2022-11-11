using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.IO;
using System;
using Disaster_Alleviation.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.AspNetCore.Mvc;
using Disaster_Alleviation.Controllers;
using Microsoft.AspNetCore.Routing;
using System.Linq;
using System.Collections.Generic;
using System.Runtime.ConstrainedExecution;

namespace DisasterUnit_Test
{
    [TestClass]
    public class UnitTest1
    {
        private IConfigurationRoot _config;

        private DbContextOptions<Disaster_Context> _options;
        private DbContextOptions<Goods_donation_Context> _GoodsOptions;
        private DbContextOptions<Monetary_donations_Context> _MoneyOptions;
        private DbContextOptions<Purchase_Context> _PurchaseOptions;


        public UnitTest1()
        {
            var builder = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json");

            _config = builder.Build();
            _options = new DbContextOptionsBuilder<Disaster_Context>()
                .UseSqlServer(_config.GetConnectionString("Online"))
                .Options;
            _config = builder.Build();

            _GoodsOptions = new DbContextOptionsBuilder<Goods_donation_Context>()
                .UseSqlServer(_config.GetConnectionString("Online"))
                .Options;

            _MoneyOptions = new DbContextOptionsBuilder<Monetary_donations_Context>()
                .UseSqlServer(_config.GetConnectionString("Online"))
                .Options;
            _PurchaseOptions = new DbContextOptionsBuilder<Purchase_Context>()
                .UseSqlServer(_config.GetConnectionString("Online"))
                .Options;
        }

        [TestMethod]
        public void InitializeDatabaseWithDisaster()
        {
            using (var context = new Disaster_Context(_options))
            {
                context.Database.EnsureCreated();

                var disaster1 = new Disaster()
                {
                    DisasterName = "Hurricane",
                    Location = "Johannesburg",
                    Description = "Heavy wind",
                    StartDate = new DateTime(2022, 11, 18),
                    EndDate = new DateTime(2022, 11, 23),
                    AidType = "Shelter",
                    Status = "Active"
                };

                context.Disaster.AddRange(disaster1);
                context.SaveChanges();
            }


        }
        [TestMethod]
        public void InitializeDatabaseWithGoods()
        {
            using (var context = new Goods_donation_Context(_GoodsOptions))
            {
                context.Database.EnsureCreated();

                var goods1 = new Goods_donations()
                {
                    Goods_Category = "Jackets",
                    Num_items = 3,
                    Goods_Description = "Thick",
                    DonationDate = new DateTime(2022, 11, 11),
                    Goods_Donor = "Dom",
                };

                context.Goods_donations.AddRange(goods1);
                context.SaveChanges();
            }
        }

        [TestMethod]
        public void InitializeDatabaseWithMontary()
        {
            using (var context = new Monetary_donations_Context(_MoneyOptions))
            {
                context.Database.EnsureCreated();

                var money1 = new Monetary_donations()
                {

                    Amount = 5000,
                    DonationDate = new DateTime(2022, 11, 11),
                    Donor = "Dom",
                };

                context.Monetary_donations.AddRange(money1);
                context.SaveChanges();
            }
        }



        public int calculateTotalPurchase()
        {
            using (var context = new Purchase_Context(_PurchaseOptions))
            {
                var purchaseTotal = context.Purchase.Where(x => x.Amount >= 0).Sum(y => y.Amount);

                return purchaseTotal;
            }
        }
            
        [TestMethod]
        public void GetTotalPurchase()
        {

            int Actual = calculateTotalPurchase();
            int expected = 1800;
            Assert.AreEqual(expected, Actual);
        }

        public int calculateTotalMoney()
        {
            using (var context = new Monetary_donations_Context(_MoneyOptions))
            {
                var total = context.Monetary_donations.Where(x => x.Amount >= 0).Sum(y => y.Amount);

                return total;
            }
        }
            
        [TestMethod]
        public void GetTotalMoney()
        {

            int Actual = calculateTotalMoney();
            int expected = 15000;
            Assert.AreEqual(expected, Actual);
        }

       

        public int calculateTotalGoods()
        {
            using (var context = new Goods_donation_Context(_GoodsOptions))
            {
                var total = context.Goods_donations.Where(x => x. Num_items>= 0).Sum(y => y.Num_items);

                return total;
            }
        }

        [TestMethod]
        public void GetTotalGoods()
        {

            int Actual = calculateTotalGoods();
            int expected = 11;
            Assert.AreEqual(expected, Actual);
        }


        public int calculateTotalMoneyLeft()
        {
            var left = calculateTotalMoney() - calculateTotalPurchase();
            return left;
            

        }

        [TestMethod]
        public void GetTotalMoneyLeft()
        {

            int Actual = calculateTotalMoneyLeft();
            int expected = 53203;
            Assert.AreEqual(expected, Actual);
        }


        public int calculateTotalAMoney()
        {
            using (var context = new Monetary_donations_Context(_MoneyOptions))
            {
                var totalMA = context.AllocateMoney.Where(x => x.Amount >= 0).Sum(y => y.Amount);

                return totalMA;
            }
        }

        [TestMethod]
        public void GetTotalAMoney()
        {

            int Actual = calculateTotalAMoney();
            int expected = 11000;
            Assert.AreEqual(expected, Actual);
        }

        public int calculateTotalAGoods()
        {
            using (var context = new Goods_donation_Context(_GoodsOptions))
            {
                var totalAG = context.AllocateGoods.Where(x => x.Num_items >= 0).Sum(y => y.Num_items);

                return totalAG;
            }
        }

        [TestMethod]
        public void GetTotalAGoods()
        {

            int Actual = calculateTotalAGoods();
            int expected = 21;
            Assert.AreEqual(expected, Actual);
        }
    }
}
