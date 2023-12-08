using Microsoft.EntityFrameworkCore;
using SmokeTestLogin.Data.Entities;
using SmokeTestLogin.Data.Utils;

namespace SmokeTestLogin.Data;

public class MainContext(DbContextOptions<MainContext> options) : DbContext(options)
{
    public DbSet<User> Users { get; set; } = null!;

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        modelBuilder.Entity<User>().Property(e => e.Id).ValueGeneratedOnAdd();
        modelBuilder.Entity<User>().HasData(SeedData.Seed());
    }
}
