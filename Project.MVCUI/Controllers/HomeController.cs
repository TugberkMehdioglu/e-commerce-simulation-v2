using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Project.ENTITIES.Models;
using System.Diagnostics;

namespace Project.MVCUI.Controllers
{
    [Route("[Controller]/[Action]")]
    public class HomeController : Controller
    {
        private readonly SignInManager<AppUser> _signInManager;

        public HomeController(SignInManager<AppUser> signInManager)
        {
            _signInManager = signInManager;
        }

        [Route("/")]
        [Route("/Home")]
        public IActionResult Index()
        {
            return View();
        }

        public async Task LogOut()
        {
            await _signInManager.SignOutAsync();
            HttpContext.Session.Remove("sessionVM");
        }

        public IActionResult Terms()
        {
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }
    }
}