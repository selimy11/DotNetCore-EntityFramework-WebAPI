using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using GJG.Entities;

namespace GJG.DataAccess
{
    public class GJGDbContext : DbContext
    {
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            base.OnConfiguring(optionsBuilder);
            optionsBuilder.UseSqlServer(@"Server=.\SQLEXPRESS; Database=GJGDB; uid=sa; pwd=A1w2e3r4t5");
        }

        public DbSet<User> Users { get; set; }
        public DbSet<Leaderboard> Leaderboards { get; set; }
    }
}
