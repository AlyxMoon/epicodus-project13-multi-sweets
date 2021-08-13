using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Identity;
using System.Threading.Tasks;

using Marketplace.Models;
using Marketplace.Models.Database;
using Marketplace.ViewModels;

namespace Marketplace.Controllers
{
  public class AccountController : Controller
  {
    private readonly DatabaseContext _db;
    private readonly UserManager<ApplicationUser> _userManager;
    private readonly SignInManager<ApplicationUser> _signInManager;

    public AccountController (
      UserManager<ApplicationUser> userManager,
      SignInManager<ApplicationUser> signInManager, 
      DatabaseContext db
    )
    {
      _userManager = userManager;
      _signInManager = signInManager;
      _db = db;
    }

    public IActionResult Register()
    {
      return View();
    }

    [HttpPost]
    public async Task<ActionResult> Register (RegisterViewModel model)
    {
      ApplicationUser user = new () { 
        Email = model.Email,
        UserName = model.Username 
      };
      IdentityResult result = await _userManager.CreateAsync(user, model.Password);

      return result.Succeeded ? RedirectToAction("Index") : View();
    }

    public ActionResult Login()
    {
      return View();
    }

    [HttpPost]
    public async Task<ActionResult> Login(LoginViewModel model)
    {
      Microsoft.AspNetCore.Identity.SignInResult result = await _signInManager.PasswordSignInAsync(
        model.Username, 
        model.Password, 
        isPersistent: true, 
        lockoutOnFailure: false
      );
      
      return result.Succeeded ? RedirectToAction("Index", "Home") : View();
    }

    public async Task<ActionResult> LogOff()
    {
      await _signInManager.SignOutAsync();
      return RedirectToAction("Index", "Home");
    }
  }
}