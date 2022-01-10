using Microsoft.EntityFrameworkCore;
using Pizza_App.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Pizza_App.Data
{
    public class MyContext:DbContext
    {
        public MyContext(DbContextOptions<MyContext> options):base(options)
        { }

        public DbSet<usuario> usuarios { get; set; }
        public DbSet<pizza> pizzas { get; set; }
        public DbSet<orden> ordenes { get; set; }
    }
}
