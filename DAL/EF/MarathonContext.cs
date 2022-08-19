using System;
using DAL.Entities;
using MarathonApp.DAL.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace MarathonApp.DAL.EF
{
    public class MarathonContext : IdentityDbContext<User>
    {
        public MarathonContext(DbContextOptions<MarathonContext> options) : base(options)
        {
            Database.EnsureCreated();
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionBuilder)
        {

        }

        public virtual DbSet<Document> Documents { get; set; }
        public virtual DbSet<Partner> Partners { get; set; }
        public virtual DbSet<Marathon> Marathons { get; set; }
        public virtual DbSet<RefreshToken> RefreshTokens { get; set; }
        public virtual DbSet<Application> Applications { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            builder.Entity<User>(u => u.Property(p => p.NewUser).HasDefaultValue(true));
            builder.Entity<Distance>()
                .HasMany(b => b.DistancePrices)
                .WithOne()
                .OnDelete(DeleteBehavior.Cascade);
            builder.Entity<Distance>()
                .HasMany(b => b.DistanceAges)
                .WithOne()
                .OnDelete(DeleteBehavior.Cascade);

        }

    }
}

