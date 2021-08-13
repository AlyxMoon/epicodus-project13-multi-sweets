using Microsoft.EntityFrameworkCore;

namespace Marketplace.Models.Database
{
  public class DatabaseContext : DbContext
  {
    public DatabaseContext(DbContextOptions options) : base(options) { }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
      optionsBuilder.UseLazyLoadingProxies();
    }
  }
}