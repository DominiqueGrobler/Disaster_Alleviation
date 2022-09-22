using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Disaster_Alleviation.Models
{
    public class Disaster_Context : DbContext
    {
        public Disaster_Context(DbContextOptions<Disaster_Context> options)
            : base(options)
        {

        }
        public DbSet<Disaster> Disaster { get; set; }
    }
}
