using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Disaster_Alleviation.Models
{
    public class Goods_donation_Context : DbContext
    {
        public Goods_donation_Context(DbContextOptions<Goods_donation_Context> options)
           : base(options)
        {

        }
        public DbSet<Goods_donations> Goods_donations { get; set; }
    }
}
