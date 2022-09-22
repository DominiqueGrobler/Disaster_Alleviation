using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Disaster_Alleviation.Models
{
    public class User_Context : DbContext
    {
        public User_Context(DbContextOptions<User_Context> options)
            : base (options)
        {

        }
        public DbSet<Users> Users { get; set; }

    }
}
