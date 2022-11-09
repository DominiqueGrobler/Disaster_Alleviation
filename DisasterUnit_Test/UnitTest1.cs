using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.IO;
using System;
using Disaster_Alleviation.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace DisasterUnit_Test
{
    [TestClass]
    public class UnitTest1
    {
        private IConfigurationRoot _config;

        private DbContextOptions<Disaster_Context> _options;
        private DbContextOptions<Goods_donation_Context> _GoodsOptions;

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
    }
}
