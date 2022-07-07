using System;
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

        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionBuilder)
        {

        }
    }
}

