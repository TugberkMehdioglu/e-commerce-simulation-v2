using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Project.BLL.ManagerServices.Abstracts;
using Project.MVCUI.ViewModels;
using Project.MVCUI.ViewModels.WrapperClasses;

namespace Project.MVCUI.Controllers
{
    [Authorize]
    [Route("[Controller]/[Action]")]
    public class ProfileController : Controller
    {
        private readonly IAppUserManager _appUserManager;
        private readonly IMapper _mapper;

        public ProfileController(IAppUserManager appUserManager, IMapper mapper)
        {
            _appUserManager = appUserManager;
            _mapper = mapper;
        }

        // GET: ProfileController/Details/5
        public async Task<IActionResult> Details(int id)
        {
            string userName = User.Identity!.Name!;

            var (isSuccess, error, appUser) = await _appUserManager.GetUserWithProfileAndAddressAsync(userName);
            if(!isSuccess)
            {
                TempData["fail"] = error;
                return RedirectToAction("Index", "Home");
                //Todo: ShoppingList Action'ı ve View'sunu yazıp, yönlendirmeyi oraya ver.
            }

            AppUserViewModel appUserViewModel = _mapper.Map<AppUserViewModel>(appUser);

            return View(appUserViewModel);
        }

        public async Task<IActionResult> Edit(int id)
        {
            var (isSuccess, error, appUser) = await _appUserManager.GetUserWithProfileAsync(User.Identity!.Name!);
            if (!isSuccess)
            {
                TempData["fail"] = error;
                return RedirectToAction(nameof(Details));
            }

            AppUserEditWrapper wrapper = new() { AppUser = _mapper.Map<AppUserViewModel>(appUser) };

            return View(wrapper);
        }

        // POST: ProfileController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: ProfileController/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: ProfileController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
    }
}
