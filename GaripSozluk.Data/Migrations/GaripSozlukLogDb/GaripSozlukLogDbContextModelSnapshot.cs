﻿// <auto-generated />
using GaripSozluk.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

namespace GaripSozluk.Data.Migrations.GaripSozlukLogDb
{
    [DbContext(typeof(GaripSozlukLogDbContext))]
    partial class GaripSozlukLogDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn)
                .HasAnnotation("ProductVersion", "3.1.8")
                .HasAnnotation("Relational:MaxIdentifierLength", 63);

            modelBuilder.Entity("GaripSozluk.Data.Domain.Log", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

                    b.Property<string>("IPAddress")
                        .HasColumnType("character varying(15)")
                        .HasMaxLength(15);

                    b.Property<string>("RequestMethod")
                        .HasColumnType("text");

                    b.Property<string>("RequestPath")
                        .HasColumnType("text");

                    b.Property<string>("ResponseStatusCode")
                        .HasColumnType("text");

                    b.Property<string>("RoutePath")
                        .HasColumnType("text");

                    b.Property<string>("TraceIdentifier")
                        .HasColumnType("text");

                    b.Property<string>("UserAgent")
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.ToTable("Logs");
                });
#pragma warning restore 612, 618
        }
    }
}
