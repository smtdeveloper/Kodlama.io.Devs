using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Persistence.Contexts
{
    public class BaseDbContext : DbContext
    {
        protected IConfiguration Configuration { get; set; }
        public DbSet<ProgrammingLanguage>  programmingLanguages { get; set; }
        public DbSet<Technology>  Technologies { get; set; }


        public BaseDbContext(DbContextOptions dbContextOptions, IConfiguration configuration) : base(dbContextOptions)
        {
            Configuration = configuration;
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            //if (!optionsBuilder.IsConfigured)
            //    base.OnConfiguring(
            //        optionsBuilder.UseSqlServer(Configuration.GetConnectionString("SomeConnectionString")));
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<ProgrammingLanguage>(a =>
            {
                a.ToTable("ProgrammingLanguages").HasKey(k => k.Id);
                a.Property(p => p.Id).HasColumnName("Id");
                a.Property(p => p.Name).HasColumnName("Name");
                a.Property(p => p.IsActive).HasColumnName("IsActive");
                a.HasMany(p => p.Technologies);

            });

            modelBuilder.Entity<Technology>(a =>
            {
                a.ToTable("Technology").HasKey(k => k.Id);
                a.Property(p => p.Id).HasColumnName("Id");
                a.Property(p => p.Name).HasColumnName("Name");
                a.Property(p => p.ProgrammingLanguageId).HasColumnName("ProgrammingLanguageId");
                a.Property(p => p.IsActive).HasColumnName("IsActive");
                a.HasOne(p => p.ProgrammingLanguage);

            });



            ProgrammingLanguage[] ProgrammingLanguageEntitySeeds = { new(1, "C#",true), new(2, "Java", true) };
            modelBuilder.Entity<ProgrammingLanguage>().HasData(ProgrammingLanguageEntitySeeds);

            Technology[] technologies = { new(1, 1, ".NET", true), new(2, 2, "Technology", true), new(3, 1, "MVC", true) };
            modelBuilder.Entity<Technology>().HasData(technologies);

        }
    }
}
