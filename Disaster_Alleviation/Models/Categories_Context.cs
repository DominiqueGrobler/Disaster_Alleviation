using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Disaster_Alleviation.Models
{
    public class Categories_Context : DbContext
        {
            public Categories_Context(DbContextOptions<Categories_Context> options)
                : base(options)
            {

            }
            public DbSet<Categories> Categories { get; set; }
        }
    
}
