using EfCoreReversing.Database.Models;
using Microsoft.EntityFrameworkCore;

namespace EfCoreReversing.Database;

internal class MyDbContext : DbContext
{
    public DbSet<Account> Accounts { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.EnableDetailedErrors();
        optionsBuilder.EnableSensitiveDataLogging();
        optionsBuilder.UseInMemoryDatabase("EFCoreReversing");
    }
}