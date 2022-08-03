using System;
using MarathonApp.DAL.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace MarathonApp.DAL.EF
{
    public class MarathonContext : IdentityDbContext<User>
    {
        public virtual DbSet<ImagesEntity> ImagesEntity { get; set; }

        public MarathonContext(DbContextOptions<MarathonContext> options) : base(options)
        {
            //Database.EnsureCreated();
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionBuilder)
        {

        }

        public DbSet<Partner> Partners { get; set; }
        public DbSet<Marathon> Marathons { get; set; }


        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            builder.Entity<User>(u => u.Property(p => p.NewUser).HasDefaultValue(true));

        }

    }
}

