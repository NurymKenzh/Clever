using CleverAPI.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CleverAPI.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        public DbSet<CATO> CATO { get; set; }

        public DbSet<CleverAPI.Models.AirContaminant> AirContaminant { get; set; }

        public DbSet<CleverAPI.Models.AirContaminantKK> AirContaminantKK { get; set; }

        public DbSet<CleverAPI.Models.AirContaminantRU> AirContaminantRU { get; set; }

        public DbSet<CleverAPI.Models.CompanyKK> CompanyKK { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<CompanyKK>()
                .HasIndex(c => c.NameRU);

            //modelBuilder.Entity<Person>()
            //.HasIndex(p => new { p.FirstName, p.LastName });
        }

        public DbSet<CleverAPI.Models.Substance> Substance { get; set; }

        public DbSet<CleverAPI.Models.Density> Density { get; set; }

        public DbSet<CleverAPI.Models.GasDensity> GasDensity { get; set; }

        public DbSet<CleverAPI.Models.CaloricEquivalent> CaloricEquivalent { get; set; }

        public DbSet<CleverAPI.Models.ParametersEmissionSource> ParametersEmissionSource { get; set; }
    }
}
