using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Simon.Models
{
    public class SimonDbContext : DbContext
    {
        public SimonDbContext(DbContextOptions<SimonDbContext> options) : base(options)
        { }

        public DbSet<User> Users { get; set; }

        public DbSet<Score> Scores { get; set; }
    }
}
