using AutoMapper;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Project.BLL.ManagerServices.Abstracts;
using Project.COMMON.Tools;
using Project.ENTITIES.Models;
using Project.MVCUI.Extensions;
using Project.MVCUI.ViewModels.WrapperClasses;

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
            else if(!isSuccess && error != null)
            {
                ModelState.AddModelErrorWithOutKey(error);
                return View(request);
            }

            string confirmationToken = await _userManager.GenerateEmailConfirmationTokenAsync(appUser);
            string confirmationLink = Url.Action("Activation", "Register", new { userId = appUser.Id, token = confirmationToken }, HttpContext.Request.Scheme)!;
            string message = $"Üyeliğiniz başarıyla oluşturuldu, hesabınızı aktive etmek için <a href='{confirmationLink}'>buraya tıklayabilirisiniz.</a>";

            MailService.SendMailAsync(appUser.Email, message, "TechByGamers | Hesap Aktivasyon");

            appUserProfile.Id = appUser.Id;
            var (success, alert) = _appUserProfileManager.AddAsync(appUserProfile);
            if (!success)
            {
                ModelState.AddModelErrorWithOutKey(alert!);
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
            if(appUser == null)
            {
                ViewBag.message = "Kullanıcı bulunamadı";
                return View();
            }

            IdentityResult result = await _userManager.ConfirmEmailAsync(appUser, token);
            if(!result.Succeeded)
            {
                foreach (IdentityError error in result.Errors)
                {
                    ViewBag.message += $"{error}\n";
                }
                return View();
            }

            ViewBag.message = "Mail adresiniz başarıyla aktive edilmiştir!";
            return View();
        }
    }
}
