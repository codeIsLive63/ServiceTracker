using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using ServiceTracker.Domain.Entities;

namespace ServiceTracker.Domain;

public class ApplicationDbContext(DbContextOptions<ApplicationDbContext> options, IConfiguration configuration) : IdentityDbContext<ApplicationUser>(options)
{
    public DbSet<Record> Records => Set<Record>();

    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);

        builder.Entity<Record>().HasData(new
        {
            RecordId = Guid.NewGuid(),
            Name = "HB CE285A",
            Date = new DateOnly(2023, 12, 27),
            ReasonDescription = "Заправка",
            ReturnStatus = ReturnStatus.PENDING
        },
        new
        {
            RecordId = Guid.NewGuid(),
            Name = "HB CE285A",
            Date = new DateOnly(2023, 12, 27),
            ReasonDescription = "Заправка",
            ReturnStatus = ReturnStatus.PENDING
        }, 
        new
        {
            RecordId = Guid.NewGuid(),
            Name = "CF217A",
            Date = new DateOnly(2023, 12, 27),
            ReasonDescription = "Заправка",
            ReturnStatus = ReturnStatus.PENDING
        });

        builder.Entity<ApplicationUser>().HasData(new
        {
            Id = Guid.NewGuid().ToString(),
            UserName = "admin",
            NormalizedUserName = "ADMIN",
            Email = "worksgak@gmail.com",
            NormalizedEmail = "WORKSGAK@GMAIL.COM",
            EmailConfirmed = true,
            PasswordHash = new PasswordHasher<ApplicationUser>().HashPassword(null, configuration["Authentication:AdminPassword"]),
            SecurityStamp = string.Empty,
            AccessFailedCount = 0,
            LockoutEnabled = false,
            PhoneNumberConfirmed = true,
            TwoFactorEnabled = false,
        });
    }
}
