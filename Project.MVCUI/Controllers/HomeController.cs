using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Project.BLL.ManagerServices.Abstracts;
using Project.BLL.ManagerServices.Concretes;
using Project.COMMON.Extensions;
using Project.ENTITIES.Models;
using Project.MVCUI.Extensions;
using Project.MVCUI.ViewModels;
using System.Diagnostics;
using SignInResult = Microsoft.AspNetCore.Identity.SignInResult;

namespace Project.MVCUI.Controllers
{
    [Route("[Controller]/[Action]")]
    public class HomeController : Controller
    {
        private readonly SignInManager<AppUser> _signInManager;
        private readonly UserManager<AppUser> _userManager;
        private readonly IAppUserProfileManager _appUserProfileManager;

        public HomeController(SignInManager<AppUser> signInManager, UserManager<AppUser> userManager, IAppUserProfileManager appUserProfileManager)
        {
            _signInManager = signInManager;
            _userManager = userManager;
            _appUserProfileManager = appUserProfileManager;
        }

        [Route("/")]
        [Route("/Home")]
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login(LoginViewModel request, string? returnUrl = null)
        {
            if (!ModelState.IsValid) return View(request);

            returnUrl ??= "/Home";

            AppUser? appUser = await _userManager.FindByEmailAsync(request.Email);
            if(appUser == null)
            {
                ModelState.AddModelErrorWithOutKey("Email veya şifre hatalı");
                return View();
            }

            SignInResult result = await _signInManager.PasswordSignInAsync(appUser, request.PasswordHash, request.RememberMe, true);
            if (result.IsLockedOut)
            {
                ModelState.AddModelErrorWithOutKey("30 dakika boyunca giriş yapamazsınız");
                return View();
            }
            else if (!result.Succeeded)
            {
                ModelState.AddModelErrorWithOutKey("Email veya şifre hatalı", $"Başarısız giriş sayısı => {await _userManager.GetAccessFailedCountAsync(appUser)}");
                return View();
            }
            
            if(!await _userManager.IsEmailConfirmedAsync(appUser))
            {
                ModelState.AddModelErrorWithOutKey("Lütfen hesabınızı aktif hale getiriniz, mail'lerinizi kontrol ediniz");
                await _signInManager.SignOutAsync();//So that the cookie still does not remain in the browser
                return View();
            }

            AppUserProfile? appUserProfile = _appUserProfileManager.Where(x => x.Id == appUser.Id).Select(x => new AppUserProfile() { Id = x.Id, ImagePath = x.ImagePath }).FirstOrDefault();
            if (appUserProfile != null)
            {
                SessionViewModel session = new() { ImagePath = appUserProfile.ImagePath };
                HttpContext.Session.SetSession("sessionVM", session);
            }

            return Redirect(returnUrl);
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
    }
}