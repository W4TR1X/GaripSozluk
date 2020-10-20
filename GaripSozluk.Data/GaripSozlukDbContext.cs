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
                new AppRole("Admin"){ Id=2, NormalizedName="ADMIN" },
                new AppRole("Bot"){ Id=3, NormalizedName="BOT" }
            });

            builder.Entity<AppUser>().HasData(new List<AppUser>() 
            {
                new AppUser()
                {
                    Id=1,
                    UserName="Mustafa",
                    NormalizedUserName="MUSTAFA" ,
                    Email=null,
                    NormalizedEmail=null,
                    EmailConfirmed=false,
                    PasswordHash    ="AQAAAAEAACcQAAAAEK3yu3mkq1NvQOijcZ0u1cvomfFXZNVO65sjGbjZcEjY1tNKYJ9VyEcG1vZ9TbY82A==", //Parola M1234d@
                    SecurityStamp   ="RZUW3UX54HAWZUMZGYM2Y3J3XG4R7FE3",
                    ConcurrencyStamp  ="f3a1e6ab-3c15-43ef-87d7-d83c22285d13",
                    PhoneNumber =null,
                    PhoneNumberConfirmed  =false,
                    TwoFactorEnabled  =false,
                    LockoutEnd =null,
                    LockoutEnabled =false,
                    AccessFailedCount =0,
                    BirthDate  = new DateTime(1986,05,07),
                    CreateDate =DateTime.Now,
                    UpdateDate=null
                },
                new AppUser()
                {
                    Id=1004,
                    UserName="HangfireBot", 
                    NormalizedUserName="HANGFIREBOT" ,
                    Email=null,
                    NormalizedEmail=null,
                    EmailConfirmed=false,
                    PasswordHash    ="AQAAAAEAACcQAAAAEK6DMSeekLJRwfEXfIbtkz5V4kOfyFabsyi+rkOSX6OzoOAYvTTOj+vnqsNMoOowLQ==", //Parola Hf1234@
                    SecurityStamp   ="XXMPKO5EG2J4S353GLJVNDBUK3JYJXET",
                    ConcurrencyStamp  ="dffe6722-61d7-4b3d-8561-4291e0695601",
                    PhoneNumber =null,
                    PhoneNumberConfirmed  =false,
                    TwoFactorEnabled  =false,
                    LockoutEnd =DateTime.MaxValue,
                    LockoutEnabled =true,
                    AccessFailedCount =0,
                    BirthDate  =DateTime.Now,
                    CreateDate =DateTime.Now,
                    UpdateDate=null
                }
            });

            builder.Entity<IdentityUserRole<int>>().HasData(new List<IdentityUserRole<int>>() {
                new IdentityUserRole<int>() { UserId = 1004, RoleId = 3 },
                new IdentityUserRole<int>() { UserId = 1, RoleId = 1 },
                 new IdentityUserRole<int>() { UserId = 1, RoleId = 2 }
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
