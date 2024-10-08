﻿using Domain.Entities.Distances;
using Domain.Entities.Languages;
using Domain.Entities.Marathons;
using Domain.Entities.Applications;
using Domain.Entities.Documents;
using Domain.Entities.Users;
using Infrastructure.Common.Extensions;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Domain.Entities.Vouchers;
using Domain.Entities.Statuses;
using Domain.Entities.Emails;

namespace Infrastructure.Persistence;

public class AppDbContext : IdentityDbContext<User, Role, long, IdentityUserClaim<long>,
    UserRole, IdentityUserLogin<long>, IdentityRoleClaim<long>,
    IdentityUserToken<long>>
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
    {
        //Database.EnsureDeleted();
        //Database.EnsureCreated();
    }
        
    public DbSet<User> Users { get; set; }
    public DbSet<RefreshToken> RefreshTokens { get; set; }
    public DbSet<Document> Documents { get; set; }
    public DbSet<Status> Statuses { get; set; }
    public DbSet<Comment> Comments { get; set; }
    public DbSet<StatusComment> StatusComments { get; set; }
    public DbSet<Application> Applications { get; set; }
    public DbSet<Language> Languages { get; set; }
    public DbSet<Marathon> Marathons { get; set; }
    public DbSet<MarathonTranslation> MarathonTranslations { get; set; }
    public DbSet<Distance> Distances { get; set; }
    public DbSet<DistanceAge> DistanceAges { get; set; }
    public DbSet<DistancePrice> DistancePrices { get; set; }
    public DbSet<Voucher> Vouchers { get; set; }
    public DbSet<Promocode> Promocodes { get; set; }
    public DbSet<PartnerCompany> PartnerCompanies { get; set; }
    public DbSet<Email> Emails { get; set; }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);
        builder.ApplyConfigurationsFromAssembly(typeof(AppDbContext).Assembly);
        
        //foreach (var x in builder.Model.GetEntityTypes().SelectMany(e => e.GetForeignKeys()))
        //    x.DeleteBehavior = DeleteBehavior.ClientCascade;
        
        builder.AddIsDeletedQuery();
    }
}