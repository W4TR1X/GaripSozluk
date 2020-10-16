using GaripSozluk.Data.Domain;
using GaripSozluk.Data.Mappings;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace GaripSozluk.Data
{
    public class GaripSozlukLogDbContext : DbContext
    {
        public GaripSozlukLogDbContext() : base() { }
        public GaripSozlukLogDbContext(DbContextOptions<GaripSozlukLogDbContext> options) : base(options) { }

        public DbSet<Log> Logs { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            base.OnConfiguring(optionsBuilder);
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.ApplyConfiguration(new LogMapping());
        }
    }
}
