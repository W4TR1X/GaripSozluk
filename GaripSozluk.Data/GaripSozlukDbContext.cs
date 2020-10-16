using GaripSozluk.Data.Domain;
using GaripSozluk.Data.Mappings;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace GaripSozluk.Data
{
    public class GaripSozlukDbContext : IdentityDbContext<AppUser, AppRole, int> //IdentityDbContext
    {
        public GaripSozlukDbContext() : base()
        {

        }
        public GaripSozlukDbContext(DbContextOptions<GaripSozlukDbContext> options) : base(options)
        {

        }

        public DbSet<Category> Categories { get; set; }
        public DbSet<Header> Headers { get; set; }
        public DbSet<Post> Posts { get; set; }
        public DbSet<PostRating> PostRatings { get; set; }
        public DbSet<BlockedUser> BlockedUsers { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            base.OnConfiguring(optionsBuilder);

        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.ApplyConfiguration(new CategoryMapping());
            builder.ApplyConfiguration(new HeaderMapping());
            builder.ApplyConfiguration(new PostMapping());
            builder.ApplyConfiguration(new PostRatingMapping());
            builder.ApplyConfiguration(new BlockedUserMapping());
            builder.ApplyConfiguration(new UserMapping());

            builder.Entity<AppRole>().HasData(new List<AppRole>()
            {
                new AppRole("User"){ Id=1, NormalizedName="USER" },
                new AppRole("Admin"){ Id=2, NormalizedName="ADMIN" }
            });

            builder.Entity<Category>().HasData(new List<Category>
            {
                new Category(){ Id=1, CreateDate=DateTime.Now, Title="gündem"},
                new Category(){ Id=2, CreateDate=DateTime.Now, Title="debe"},
                new Category(){ Id=3, CreateDate=DateTime.Now, Title="sorunsallar"},
                new Category(){ Id=4, CreateDate=DateTime.Now, Title="#spor"},
                new Category(){ Id=5, CreateDate=DateTime.Now, Title="#ilişkiler"},
                new Category(){ Id=6, CreateDate=DateTime.Now, Title="#siyaset"}
            });
        }
    }
}
