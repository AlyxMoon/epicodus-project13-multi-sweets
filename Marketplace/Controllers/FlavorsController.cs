using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Authorization;

using Marketplace.Models;
using Marketplace.Models.Database;

namespace Marketplace.Controllers
{
  [Route("/flavors")]
  public class FlavorsController : Controller
  {
    private readonly DatabaseContext _db;
    private readonly UserManager<ApplicationUser> _userManager;

    public FlavorsController (
      UserManager<ApplicationUser> userManager,
      DatabaseContext db
    )
    {
      _userManager = userManager;
      _db = db;
    }

    [HttpGet]
    public ActionResult Index()
    {
      List<Flavor> model = _db.Flavors.ToList();
      return View(model);
    }
  
    [Authorize]
    [HttpGet("new")]
    public ActionResult AddNew()
    {
      return View();
    }

    [Authorize]
    [HttpPost]
    public ActionResult Create(Flavor flavor)
    {
      _db.Flavors.Add(flavor);
      _db.SaveChanges();
      return RedirectToAction("Index");
    }

    [HttpGet("{id}")]
    public ActionResult Details (int id)
    {
      Flavor flavor = _db.Flavors
        .Include(item => item.Treats)
        .SingleOrDefault(item => item.FlavorId == id);      

      IEnumerable<Treat> treats = _db.Treats
        .AsEnumerable()
        .Where(item => flavor.Treats.All(flavorTreat => 
          flavorTreat.TreatId != item.TreatId
        ));

      ViewBag.TreatOptions = new SelectList(treats, "TreatId", "Name");

      return View(flavor);
    }

    [Authorize]
    [HttpGet("{id}/edit")]
    public ActionResult Edit(int id)
    {
      Flavor flavor = _db.Flavors.FirstOrDefault(flavor => flavor.FlavorId == id);
      return View(flavor);
    }

    [Authorize]
    [HttpPost("{id}")]
    public ActionResult EditPost (Flavor flavor)
    {
      _db.Entry(flavor).State = EntityState.Modified;
      _db.SaveChanges();
      return RedirectToAction("Index");
    }

    [Authorize]
    [HttpPost("{flavorId}/treats/add")]
    public ActionResult AddTreat (int flavorId, int treatId)
    {
      Flavor flavor = _db.Flavors
        .Include(flavor => flavor.Treats)
        .SingleOrDefault(flavor => flavor.FlavorId == flavorId);

      Treat treat = _db.Treats
        .SingleOrDefault(treat => treat.TreatId == treatId);

      flavor.Treats.Add(treat);
      _db.SaveChanges();

      return RedirectToAction("Details", new { id = flavorId });
    }

    [Authorize]
    [HttpGet("{id}/delete")]
    public ActionResult Delete (int id)
    {
      Flavor flavor = _db.Flavors.FirstOrDefault(flavor => flavor.FlavorId == id);
      _db.Flavors.Remove(flavor);
      _db.SaveChanges();

      return RedirectToAction("Index");
    }

    [Authorize]
    [HttpGet("{flavorId}/treats/remove/{treatId}")]
    public ActionResult DeleteTreat (int flavorId, int treatId)
    {
      Flavor flavor = _db.Flavors
        .Include(flavor => flavor.Treats)
        .SingleOrDefault(flavor => flavor.FlavorId == flavorId);

      Treat treat = _db.Treats
        .SingleOrDefault(treat => treat.TreatId == treatId);

      flavor.Treats.Remove(treat);
      _db.SaveChanges();

      return RedirectToAction("Details", new { id = flavorId });
    }
  }
}