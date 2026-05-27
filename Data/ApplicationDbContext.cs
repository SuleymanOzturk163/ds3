using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ds3.Models;
using Microsoft.EntityFrameworkCore;

namespace ds3.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { }

        public DbSet<Location> Lokasyon { get; set; }

        public DbSet<rehber> rehberim { get; set; }

        public DbSet<Boss> Bosslar { get; set; }

        public DbSet<Uye> Uyeler { get; set; }

        public DbSet<yorum> yorumlar { get; set; }


    }
}