using Microsoft.EntityFrameworkCore;
using PratiBus.Domain.Entities;

namespace Infrastructure;

public class ApplicationDbContext : DbContext
{
    public DbSet<Company> Company { get; set; }
    public DbSet<Driver> Driver { get; set; }

    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
    {
    }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseNpgsql();
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(ApplicationDbContext).Assembly);
    }
}
