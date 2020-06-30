using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using SamuraiApp.Domain;
using System;

namespace SamuraiApp.Data
{
    public class SamuraiContext : DbContext
    {
        public SamuraiContext(DbContextOptions<SamuraiContext> options) : base(options)
        {
            ChangeTracker.QueryTrackingBehavior = QueryTrackingBehavior.NoTracking;
        }
        public DbSet<Samurai> Samurais { get; set; }
        public DbSet<Quote> Quotes { get; set; }
        public DbSet<Clan> Clans { get; set; }
        public DbSet<Battle> Battles { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<SamuraiBattle>()
                .HasKey(s => new { s.SamuraiId, s.BattleId });
            modelBuilder.Entity<Horse>()
                .ToTable("Horses");
        }

        //public static readonly ILoggerFactory ConsoleLoggerFactory
        //    = LoggerFactory.Create(builder =>
        //   {
        //       builder
        //           .AddFilter((category, level) =>
        //               category == DbLoggerCategory.Database.Command.Name
        //               && level == LogLevel.Information)
        //           .AddConsole();
        //   });

        //protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        //{
        //    optionsBuilder
        //        .UseLoggerFactory(ConsoleLoggerFactory)
        //        .EnableSensitiveDataLogging()
        //        .UseSqlServer("Data Source=(localdb)\\ProjectsV13;Initial Catalog=SamuraiAppData");
        //}
    }
}
