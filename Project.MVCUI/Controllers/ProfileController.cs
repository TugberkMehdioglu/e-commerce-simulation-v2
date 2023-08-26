using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Project.BLL.ManagerServices.Abstracts;
using Project.COMMON.Extensions;
using Project.ENTITIES.Models;
using Project.MVCUI.Extensions;
using Project.MVCUI.ViewModels;
using Project.MVCUI.ViewModels.WrapperClasses;

namespace Project.MVCUI.Controllers
{
    [Authorize]
    [Route("[Controller]/[Action]")]
    public class ProfileController : Controller
    {
        private readonly IAppUserManager _appUserManager;
        private readonly IAppUserProfileManager _appUserProfileManager;
        private readonly UserManager<AppUser> _userManager;
        private readonly IMapper _mapper;

        public ProfileController(IAppUserManager appUserManager, IMapper mapper, IAppUserProfileManager appUserProfileManager, UserManager<AppUser> userManager)
        {
            _appUserManager = appUserManager;
            _mapper = mapper;
            _appUserProfileManager = appUserProfileManager;
            _userManager = userManager;
        }

        // GET: ProfileController/Details/5
        public async Task<IActionResult> Details(int id)
        {
            string userName = User.Identity!.Name!;

            var (isSuccess, error, appUser) = await _appUserManager.GetUserWithProfileAndAddressAsync(userName);
            if(!isSuccess)
            {
                TempData["fail"] = error;
                return RedirectToAction("Index", "Shopping");
            }

            AppUserViewModel appUserViewModel = _mapper.Map<AppUserViewModel>(appUser);

            if (TempData["success"] != null) TempData["success"] = TempData["success"];

            return View(appUserViewModel);
        }

        public async Task<IActionResult> Edit()
        {
            var (isSuccess, error, appUser) = await _appUserManager.GetUserWithProfileAsync(User.Identity!.Name!);
            if (!isSuccess)
            {
                TempData["fail"] = error;
                return RedirectToAction(nameof(Details));
            }

            AppUserEditWrapper wrapper = new() { AppUser = _mapper.Map<AppUserViewModel>(appUser) };

            IEnumerable<IdentityError>? errors = HttpContext.Session.GetSession<IEnumerable<IdentityError>>("identityErrors");

            if (errors != null)
            {
                HttpContext.Session.Remove("identityErrors");
                ModelState.AddModelErrorListWithOutKey(errors);
            }
            else if (TempData["error"] != null) ModelState.AddModelErrorWithOutKey((TempData["error"]!.ToString())!);

            return View(wrapper);
        }

        // POST: ProfileController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(AppUserEditWrapper request)
        {
            //Şifre Değiştir form tag'i başka bir Action'a yönleneceği için bu validation'ları pas geçtik
            ModelState.Remove("AppUser.PasswordHash");
            ModelState.Remove("AppUser.PasswordHashConfirm");
            if (!ModelState.IsValid) return View(request);

            AppUser appUser = _mapper.Map<AppUser>(request.AppUser);
            AppUserProfile appUserProfile = _mapper.Map<AppUserProfile>(request.AppUser!.AppUserProfile);

            var (isSuccess, error, errors) = await _appUserManager.EditUserWithOutPictureAsync(appUser);
            if (!isSuccess && errors != null)
            {
                ModelState.AddModelErrorListWithOutKey(errors);
                return View(request);
            }
            else if (!isSuccess && errors == null)
            {
                ModelState.AddModelErrorWithOutKey(error!);
                return View(request);
            }

            var (Success, profileError) = await _appUserProfileManager.UpdateAsync(appUserProfile);
            if (!isSuccess)
            {
                ModelState.AddModelErrorWithOutKey(profileError!);
                return View(request);
            }

            TempData["success"] = "Profil güncellendi";
            return RedirectToAction(nameof(Details));
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> ChangePassword(AppUserEditWrapper request)
        {
            if (!ModelState.IsValid) return View();

            AppUser appUser = await _userManager.FindByNameAsync(User.Identity!.Name);

            if (!await _userManager.CheckPasswordAsync(appUser, request.PasswordChange!.Password))
            {
                TempData["fail"] = "Mevcut şifreniz yanlış";
                return RedirectToAction(nameof(Edit));
            }

            var (isSuccess, identityErrors, error) = await _appUserManager.ChangePasswordAsync(appUser, request.PasswordChange!.Password, request.PasswordChange.NewPassword);
            if (!isSuccess && identityErrors != null)
            {
                HttpContext.Session.SetSession("identityErrors", identityErrors);
                return RedirectToAction(nameof(Edit));
            }
            else if (!isSuccess && error != null)
            {
                TempData["error"] = error;
                return RedirectToAction(nameof(Edit));
            }

            TempData["success"] = "Şifreniz başarıyla değiştirildi";
            return RedirectToAction(nameof(Details));
        }
    }
}
