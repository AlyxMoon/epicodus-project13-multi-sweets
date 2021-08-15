using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc.Rendering;

using Marketplace.Models;
using Marketplace.Models.Database;

namespace Marketplace.Controllers
{
  public class FlavorsController : Controller
  {
    private readonly DatabaseContext _db;

    public FlavorsController (DatabaseContext db)
    {
      _db = db;
    }

    public ActionResult Index()
    {
      List<Flavor> model = _db.Flavors.ToList();
      return View(model);
    }

    public ActionResult Details (int id)
    {
      Flavor flavor = _db.Flavors.FirstOrDefault(flavor => flavor.FlavorId == id);
      return View(flavor);
    }

    public ActionResult Create()
    {
      return View();
    }

    [HttpPost]
    public ActionResult Create(Flavor flavor)
    {
      _db.Flavors.Add(flavor);
      _db.SaveChanges();
      return RedirectToAction("Index");
    }

    public ActionResult Edit(int id)
    {
      Flavor flavor = _db.Flavors.FirstOrDefault(flavor => flavor.FlavorId == id);
      return View(flavor);
    }

    [HttpPost]
    public ActionResult Edit (Flavor flavor)
    {
      _db.Entry(flavor).State = EntityState.Modified;
      _db.SaveChanges();
      return RedirectToAction("Index");
    }

    public ActionResult AddTreat (int id)
    {
      Flavor flavor = _db.Flavors.FirstOrDefault(flavor => flavor.FlavorId == id);
      ViewBag.TreatOptions = new SelectList(_db.Treats, "TreatId", "Name");
      return View(flavor);
    }

    [HttpPost]
    public ActionResult AddTreat (int flavorId, int treatId)
    {
      Flavor flavor = _db.Flavors
        .Include(flavor => flavor.Treats)
        .SingleOrDefault(flavor => flavor.FlavorId == flavorId);

      Treat treat = _db.Treats
        .SingleOrDefault(treat => treat.TreatId == treatId);

      flavor.Treats.Add(treat);
      _db.SaveChanges();

      return RedirectToAction("Index");
    }

    public ActionResult Delete (int id)
    {
      Flavor flavor = _db.Flavors.FirstOrDefault(flavor => flavor.FlavorId == id);
      _db.Flavors.Remove(flavor);
      _db.SaveChanges();

      return RedirectToAction("Index");
    }

    [HttpPost]
    public ActionResult DeleteTreat (int flavorId, int treatId)
    {
      Flavor flavor = _db.Flavors
        .Include(flavor => flavor.Treats)
        .SingleOrDefault(flavor => flavor.FlavorId == flavorId);

      Treat treat = _db.Treats
        .SingleOrDefault(treat => treat.TreatId == treatId);

      flavor.Treats.Remove(treat);
      _db.SaveChanges();
      return RedirectToAction("Index");
    }
  }
}