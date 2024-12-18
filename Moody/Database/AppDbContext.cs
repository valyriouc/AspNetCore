using Microsoft.EntityFrameworkCore;
using Moody.Models;

namespace Moody.Database;

public class AppDbContext : DbContext
{
    public DbSet<AccountModel> Accounts { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        base.OnConfiguring(optionsBuilder);
        optionsBuilder.UseInMemoryDatabase("Moody");
    }
}