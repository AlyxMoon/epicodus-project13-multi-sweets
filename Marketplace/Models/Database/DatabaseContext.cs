using Microsoft.EntityFrameworkCore;

using Marketplace.Models;

namespace Marketplace.Models.Database
{
  public class DatabaseContext : DbContext
  {
    public DbSet<Flavor> Flavors { get; set; }
    public DbSet<Treat> Treats { get; set; }

    public DatabaseContext(DbContextOptions options) : base(options) { }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
      optionsBuilder.UseLazyLoadingProxies();
    }
  }
}