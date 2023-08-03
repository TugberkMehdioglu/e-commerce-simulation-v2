using AutoMapper;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.WebUtilities;
using Project.BLL.ManagerServices.Abstracts;
using Project.COMMON.Tools;
using Project.ENTITIES.Models;
using Project.MVCUI.Extensions;
using Project.MVCUI.ViewModels.WrapperClasses;
using System.Text;
using System.Web;

namespace Project.MVCUI.Controllers
{
    [Route("[Controller]/[Action]")]
    public class RegisterController : Controller
    {
        private readonly IAppUserManager _appUserManager;
        private readonly IAppUserProfileManager _appUserProfileManager;
        private readonly IMapper _mapper;
        private readonly UserManager<AppUser> _userManager;

        public RegisterController(IAppUserManager appUserManager, IAppUserProfileManager appUserProfileManager, IMapper mapper, UserManager<AppUser> userManager)
        {
            _appUserManager = appUserManager;
            _appUserProfileManager = appUserProfileManager;
            _mapper = mapper;
            _userManager = userManager;
        }

        public IActionResult SignUp()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> SignUp(SignUpWrapper request)
        {
            if (!ModelState.IsValid) return View(request);

            bool? checkBox = Request.Form.Keys.Contains("checkBox");
            if (checkBox != true)
            {
                ModelState.AddModelErrorWithOutKey("Üyelik sözleşmesini kabul etmelisiniz");
                return View(request);
            }

            AppUser appUser = _mapper.Map<AppUser>(request.AppUser);
            AppUserProfile appUserProfile = _mapper.Map<AppUserProfile>(request.AppUserProfile);

            appUser.Id = Guid.NewGuid().ToString();//https://stackoverflow.com/questions/59134406/unable-to-track-an-entity-of-type-because-primary-key-property-id-is-null
            var (isSuccess, errors, error) = await _appUserManager.AddUserByIdentityAsync(appUser);
            if (!isSuccess && errors != null)
            {
                ModelState.AddModelErrorListWithOutKey(errors);
                return View(request);
            }
            else if (!isSuccess && error != null)
            {
                ModelState.AddModelErrorWithOutKey(error);
                return View(request);
            }

            string confirmationToken = await _userManager.GenerateEmailConfirmationTokenAsync(appUser);
            //Oluşan token'da özel karakterler veya Url ile uyumsuz karakterler olmasına karşın Encode'ladık, Activation action'ında da Decode'ladık.
            string encodedToken = HttpUtility.UrlEncode(confirmationToken);
            string confirmationLink = Url.Action("Activation", "Register", new { userId = appUser.Id, token = encodedToken }, HttpContext.Request.Scheme)!;
            string message = $@"
  <div style='max-width: 600px; margin: 0 auto; padding: 20px; border: 1px solid #ccc; border-radius: 10px; padding: 20px; box-shadow: 0 0 10px rgba(0, 0, 0, 0.1);'>
    <div style='text-align: center;'>
        <img src='https://cdn0.iconfinder.com/data/icons/web-seo-outline-1/32/user_interface_OUTLINE_GRADIENT-01-512.png' alt='Logo' style='width: 15%; height: auto;'>
    </div>
    <div style='margin-top: 30px;'>
        <h2 style='text-align: center; color: rgb(46, 74, 150); font-family: sans-serif;'>Aramıza Hoşgeldiniz!</h2>
        <p style='text-align: center;'><a href='{confirmationLink}' style='color:#fff; display: inline-block;padding: 10px 20px;background-color: rgb(46, 74, 150);text-decoration: none;border-radius: 5px; font-family: sans-serif;'>Üyeliğinizi aktifleştirmek için tıklayınız</a></p>

        <div style='text-align: center; font-family: sans-serif;'>
          <p>Merhaba {appUserProfile.FullName}, hesabınız başarıyla oluşturuldu.</p>
          <p>Teknolojinin yeni dünyası <strong>TechByGamers.com</strong>'a hoş geldiniz!</p>
          <p>Üyelik işleminizin tamamlanabilmesi için yukarıdaki bağlantıdan hesabınızı aktif etmeniz gerekmektedir.</p>
        </div>
    </div>
</div>
            ";

            MailService.SendMailAsync(appUser.Email, message, "TechByGamers | Hesap Aktivasyon");

            appUserProfile.Id = appUser.Id;
            var (success, alert) = await _appUserProfileManager.AddAsync(appUserProfile);
            if (!success)
            {
                ModelState.AddModelErrorWithOutKey(alert!);
                return View(request);
            }

            IdentityResult result = await _userManager.AddToRoleAsync(appUser, "Member");
            if (!result.Succeeded)
            {
                ModelState.AddModelErrorListWithOutKey(result.Errors);
                return View(request);
            }

            return RedirectToAction(nameof(RegistrationOk), "Register", new { email = appUser.Email });
        }

        [HttpGet("{email}")]
        public IActionResult RegistrationOk(string email)
        {
            ViewBag.email = email;
            return View();
        }

        [HttpGet("{userId}/{token}")]
        public async Task<IActionResult> Activation(string userId, string token)
        {
            AppUser? appUser = await _userManager.FindByIdAsync(userId);
            if (appUser == null)
            {
                ViewBag.message = "Kullanıcı bulunamadı";
                return View();
            }

            string decodedToken = HttpUtility.UrlDecode(token);
            IdentityResult result = await _userManager.ConfirmEmailAsync(appUser, decodedToken);
            if (!result.Succeeded)
            {
                foreach (IdentityError error in result.Errors)
                {
                    ViewBag.message += $"{error.Description}\n";
                }
                return View();
            }

            ViewBag.message = "Mail adresiniz başarıyla aktive edilmiştir!";
            return View();
        }
    }
}
