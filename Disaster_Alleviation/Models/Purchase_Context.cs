using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Disaster_Alleviation.Models
{
    public class Purchase_Context : DbContext
        {
        public Purchase_Context(DbContextOptions<Purchase_Context> options)
                : base(options)
            {

            }
            public DbSet<Purchase> Purchase { get; set; }
        }
    
}
