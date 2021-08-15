using Microsoft.AspNetCore.Mvc;

namespace Marketplace.Controllers
{
    public class HomeController : Controller
    {
      [HttpGet("/")]
      public ActionResult Index()
      {
        return View();
      }
    }
}