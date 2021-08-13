
namespace Marketplace.Models
{
  public class Flavor
  {
    public int FlavorId { get; set; }

    public string Name { get; set; }

    public string Description { get; set; }

    public virtual System.Collections.Generic.IList<Treat> Treats { get; set; }
  }
}