using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using Project.BLL.ManagerServices.Abstracts;
using Project.BLL.ManagerServices.Concretes;
using Project.COMMON.Extensions;
using Project.COMMON.Tools;
using Project.ENTITIES.Models;
using Project.MVCUI.Extensions;
using Project.MVCUI.ViewModels;
using System.Diagnostics;
using System.Web;
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

        public IActionResult ForgetPassword()
        {
            if (TempData["resetPasswordAlert"] != null) ModelState.AddModelErrorWithOutKey(TempData["resetPasswordAlert"]!.ToString()!);

            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> ForgetPassword(ForgetPasswordViewModel request)
        {
            ModelState.Remove("NewPassword");
            ModelState.Remove("NewPasswordConfirm");
            if (!ModelState.IsValid) return View(request);

            AppUser? appUser = await _userManager.FindByEmailAsync(request.Email);
            if (appUser == null)
            {
                ModelState.AddModelErrorWithOutKey("Bu email adresine sahip kullanıcı bulunamadı");
                return View(request);
            }

            string passwordResetToken = await _userManager.GeneratePasswordResetTokenAsync(appUser!);
            string encodedToken = HttpUtility.UrlEncode(passwordResetToken);
            string passwordResetLink = Url.Action("ResetPassword", "Home", new { userId = appUser!.Id, token = encodedToken }, HttpContext.Request.Scheme)!;
            string emailBody= $@"
  <div style='max-width: 600px; margin: 0 auto; padding: 20px; border: 1px solid #ccc; border-radius: 10px; padding: 20px; box-shadow: 0 0 10px rgba(0, 0, 0, 0.1);'>
    <div style='text-align: center;'>
        <img src='https://cdn-icons-png.flaticon.com/512/6357/6357059.png' alt='Logo' style='width: 15%; height: auto;'>
    </div>
    <div style='margin-top: 30px;'>
        <h2 style='text-align: center; color: rgb(46, 74, 150); font-family: sans-serif;'>Şifre değiştirme talebini aldık!</h2>
        <p style='text-align: center;'><a href='{passwordResetLink}' style='color:#fff; display: inline-block;padding: 10px 20px;background-color: rgb(46, 74, 150);text-decoration: none;border-radius: 5px; font-family: sans-serif;'>Şifremi Değiştir</a></p>

        <div style='text-align: center; font-family: sans-serif;'>
          <p>Eğer bu talebi siz gerçekleştirmediyseniz herhangi bir işlem yapmanıza gerek yoktur.</p>
        </div>
    </div>
</div>
            ";

            MailService.SendMailAsync(appUser.Email, emailBody, "Şifre Sıfırlama | TechByGamers");
            TempData["success"] = "Şifre sıfırlama linki email adresinize gönderilmiştir";
            return View();
        }

        [HttpGet("{userId}/{token}")]
        public IActionResult ResetPassword(string userId, string token)
        {
            if(userId == null || token == null)
            {
                TempData["resetPasswordAlert"] = "Geçersiz kullanıcı yada zaman aşımı gerçekleşti";
                return RedirectToAction(nameof(ForgetPassword));
            }

            string encodedToken = HttpUtility.UrlDecode(token);

            TempData["userId"] = userId;
            TempData["token"] = encodedToken;

            IEnumerable<IdentityError>? errors = HttpContext.Session.GetSession<IEnumerable<IdentityError>>("resetPasswordAlers");
            if(errors != null)
            {
                ModelState.AddModelErrorListWithOutKey(errors);
                HttpContext.Session.Remove("resetPasswordAlers");
            }

            return View();
        }

        [HttpPost("{userId}/{token}")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> ResetPassword(ForgetPasswordViewModel request)
        {
            ModelState.Remove("Email");
            if (!ModelState.IsValid) return View();

            object? userId = TempData["userId"];
            object? token = TempData["token"];
            if(userId == null || token == null)
            {
                TempData["resetPasswordAlert"] = "Geçersiz kullanıcı yada zaman aşımı gerçekleşti";
                return RedirectToAction(nameof(ForgetPassword));
            }

            AppUser? appUser = await _userManager.FindByIdAsync(userId.ToString());
            if(appUser == null)
            {
                TempData["resetPasswordAlert"] = "Kullanıcı bulunamadı";
                return RedirectToAction(nameof(ForgetPassword));
            }

            IdentityResult result = await _userManager.ResetPasswordAsync(appUser, token.ToString(), request.NewPassword);
            if (!result.Succeeded)
            {
                string encodedToken = HttpUtility.UrlEncode(token.ToString()!);
                HttpContext.Session.SetSession("resetPasswordAlers", result.Errors);
                return RedirectToAction(nameof(ResetPassword), "Home", new { userId = userId.ToString(), token = encodedToken });
            }

            TempData["success"] = "Şifreniz başarıyla yenilendi!";
            return RedirectToAction(nameof(Login));
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