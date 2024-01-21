namespace SmokeTestLogin.Data;

public sealed class MainContext(DbContextOptions<MainContext> options) : DbContext(options)
{
    public DbSet<User> Users => Set<User>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        modelBuilder.Entity<User>().Property(e => e.Id).ValueGeneratedOnAdd();
        modelBuilder.Entity<User>();
    }
}
