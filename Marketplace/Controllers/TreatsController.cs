using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc.Rendering;

using Marketplace.Models;
using Marketplace.Models.Database;

namespace Marketplace.Controllers
{
  public class TreatsController : Controller
  {
    private readonly DatabaseContext _db;

    public TreatsController (DatabaseContext db)
    {
      _db = db;
    }

    public ActionResult Index()
    {
      List<Treat> model = _db.Treats.ToList();
      return View(model);
    }

    public ActionResult Details (int id)
    {
      Treat treat = _db.Treats.FirstOrDefault(flavor => flavor.TreatId == id);
      return View(treat);
    }

    public ActionResult Create()
    {
      return View();
    }

    [HttpPost]
    public ActionResult Create(Treat treat)
    {
      _db.Treats.Add(treat);
      _db.SaveChanges();
      return RedirectToAction("Index");
    }

    public ActionResult Edit(int id)
    {
      Treat treat = _db.Treats.FirstOrDefault(treat => treat.TreatId == id);
      return View(treat);
    }

    [HttpPost]
    public ActionResult Edit(Treat treat)
    {
      _db.Entry(treat).State = EntityState.Modified;
      _db.SaveChanges();
      return RedirectToAction("Index");
    }

    public ActionResult AddFlavor (int id)
    {
      Treat treat = _db.Treats.FirstOrDefault(treat => treat.TreatId == id);
      ViewBag.FlavorOptions = new SelectList(_db.Flavors, "FlavorId", "Name");
      return View(treat);
    }

    [HttpPost]
    public ActionResult AddFlavor (int treatId, int flavorId)
    {
      Treat treat = _db.Treats
        .Include(treat => treat.Flavors)
        .SingleOrDefault(treat => treat.TreatId == treatId);

      Flavor flavor = _db.Flavors
        .SingleOrDefault(flavor => flavor.FlavorId == flavorId);

      treat.Flavors.Add(flavor);
      _db.SaveChanges();

      return RedirectToAction("Index");
    }

    public ActionResult Delete (int id)
    {
      Treat treat = _db.Treats.FirstOrDefault(treat => treat.TreatId == id);
      _db.Treats.Remove(treat);
      _db.SaveChanges();

      return RedirectToAction("Index");
    }

    [HttpPost]
    public ActionResult DeleteFlavor (int treatId, int flavorId)
    {
      Treat treat = _db.Treats
        .Include(flavor => flavor.Flavors)
        .SingleOrDefault(treat => treat.TreatId == treatId);

      Flavor flavor = _db.Flavors
        .SingleOrDefault(flavor => flavor.FlavorId == flavorId);

      treat.Flavors.Remove(flavor);
      _db.SaveChanges();
      return RedirectToAction("Index");
    }
  }
}