
namespace Marketplace.Models
{
  public class Treat
  {
    public int TreatId { get; set; }

    public string Name { get; set; }

    public string Description { get; set; }

    public virtual System.Collections.Generic.IList<Flavor> Flavors { get; set; }
  }
}