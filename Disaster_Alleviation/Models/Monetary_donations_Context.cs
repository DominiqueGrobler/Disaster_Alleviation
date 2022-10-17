using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Disaster_Alleviation.Models
{
    public class Monetary_donations_Context : DbContext
    {
        public Monetary_donations_Context(DbContextOptions<Monetary_donations_Context> options)
           : base(options)
        {

        }
        public DbSet<Monetary_donations> Monetary_donations { get; set; }
        public DbSet<AllocateMoney> AllocateMoney { get; set; }
    }

}
