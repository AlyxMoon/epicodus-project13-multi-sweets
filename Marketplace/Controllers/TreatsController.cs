using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc.Rendering;

using Marketplace.Models;
using Marketplace.Models.Database;

namespace Marketplace.Controllers
{
  [Route("/treats")]
  public class TreatsController : Controller
  {
    private readonly DatabaseContext _db;

    public TreatsController (DatabaseContext db)
    {
      _db = db;
    }

    [HttpGet]
    public ActionResult Index()
    {
      List<Treat> model = _db.Treats.ToList();
      return View(model);
    }

    [HttpGet("new")]
    public ActionResult AddNew()
    {
      return View();
    }

    [HttpPost]
    public ActionResult Create (Treat treat)
    {
      _db.Treats.Add(treat);
      _db.SaveChanges();
      return RedirectToAction("Index");
    }

    [HttpGet("{id}")]
    public ActionResult Details (int id)
    {
      Treat treat = _db.Treats
        .Include(treat => treat.Flavors)
        .SingleOrDefault(item => item.TreatId == id);      

      IEnumerable<Flavor> flavors = _db.Flavors
        .AsEnumerable()
        .Where(flavor => treat.Flavors.All(flavorTreat => 
          flavorTreat.FlavorId != flavor.FlavorId
        ));

      ViewBag.FlavorOptions = new SelectList(flavors, "FlavorId", "Name");

      return View(treat);
    }

    [HttpGet("{id}/edit")]
    public ActionResult Edit (int id)
    {
      Treat treat = _db.Treats.FirstOrDefault(treat => treat.TreatId == id);
      return View(treat);
    }

    [HttpPost("{id}")]
    public ActionResult EditPost (Treat treat)
    {
      _db.Entry(treat).State = EntityState.Modified;
      _db.SaveChanges();
      return RedirectToAction("Index");
    }

    [HttpPost("{treatId}/flavors/add")]
    public ActionResult AddFlavor (int treatId, int flavorId)
    {
      Treat treat = _db.Treats
        .Include(treat => treat.Flavors)
        .SingleOrDefault(treat => treat.TreatId == treatId);

      Flavor flavor = _db.Flavors
        .SingleOrDefault(flavor => flavor.FlavorId == flavorId);

      treat.Flavors.Add(flavor);
      _db.SaveChanges();

      return RedirectToAction("Details", new { id = treatId });
    }

    [HttpGet("{id}/delete")]
    public ActionResult Delete (int id)
    {
      Treat treat = _db.Treats.FirstOrDefault(treat => treat.TreatId == id);
      _db.Treats.Remove(treat);
      _db.SaveChanges();

      return RedirectToAction("Index");
    }

    [HttpGet("{treatId}/flavors/delete/{flavorId}")]
    public ActionResult DeleteFlavor (int treatId, int flavorId)
    {
      Treat treat = _db.Treats
        .Include(flavor => flavor.Flavors)
        .SingleOrDefault(treat => treat.TreatId == treatId);

      Flavor flavor = _db.Flavors
        .SingleOrDefault(flavor => flavor.FlavorId == flavorId);

      treat.Flavors.Remove(flavor);
      _db.SaveChanges();
      
      return RedirectToAction("Details", new { id = treatId });
    }
  }
}