using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Project.BLL.ManagerServices.Abstracts;
using Project.ENTITIES.Enums;
using Project.ENTITIES.Models;
using Project.MVCUI.Areas.Admin.AdminViewModels;
using Project.MVCUI.Extensions;

namespace Project.MVCUI.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Route("[Area]/[Controller]/[Action]")]
    [Authorize(Roles = "Admin")]
    public class UserController : Controller
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly IAppUserManager _appUserManager;
        public UserController(UserManager<AppUser> userManager, IAppUserManager appUserManager, RoleManager<IdentityRole> roleManager)
        {
            _userManager = userManager;
            _appUserManager = appUserManager;
            _roleManager = roleManager;
        }

        public async Task<IActionResult> Index()
        {
            List<UserViewModel> users = await _userManager.Users.Where(x => x.Status != DataStatus.Deleted).Select(x => new UserViewModel()
            {
                Id = x.Id,
                UserName = x.UserName,
                Email = x.Email,
                PhoneNumber = x.PhoneNumber,
                EmailConfirmed = x.EmailConfirmed
            }).ToListAsync();

            foreach (UserViewModel user in users)
            {
                user.Roles = await _userManager.GetRolesAsync(new() { Id = user.Id, UserName = user.UserName });
            }

            return View(users);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> AssignRoleToUser(string id)
        {
            AppUser appUser = await _userManager.FindByIdAsync(id);
            if (appUser == null)
            {
                ModelState.AddModelErrorWithOutKey("Kullanıcı bulunamadı");
                return View();
            }
            TempData["userId"] = appUser.Id;

            List<IdentityRole> roles = await _roleManager.Roles.ToListAsync();
            IList<string> userRoles = await _userManager.GetRolesAsync(appUser);

            List<AssignRoleToUserViewModel> userRolesViewModel = new();
            foreach (IdentityRole role in roles)
            {
                AssignRoleToUserViewModel roleViewModel = new()
                {
                    Id = role.Id,
                    Name = role.Name
                };
                if (userRoles.Contains(role.Name)) roleViewModel.Exist = true;

                if (role.Name == "Admin" && appUser.Id == "5c8defd5-91f2-4256-9f16-e7fa7546dec4") continue;

                userRolesViewModel.Add(roleViewModel);
            }

            return View(userRolesViewModel);
        }

        [HttpPost("{id}")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> AssignRoleToUser(List<AssignRoleToUserViewModel> requestList)
        {
            if (!ModelState.IsValid) return View();

            string userId = TempData["userId"]!.ToString()!;

            AppUser? appUser = await _userManager.FindByIdAsync(userId);
            if (appUser == null)
            {
                TempData["fail"] = "Atama yapılacak kullanıcı bulunamadı";
                return RedirectToAction(nameof(Index), "User");
            }

            foreach (AssignRoleToUserViewModel item in requestList)
            {
                if (item.Exist) await _userManager.AddToRoleAsync(appUser, item.Name);
                else await _userManager.RemoveFromRoleAsync(appUser, item.Name);
            }

            TempData["success"] = "Rol atama başarıyla gerçekleşti";
            return RedirectToAction(nameof(Index), "User", new { Area = "Admin" });
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> DeleteUser(string id)
        {
            if (id == "5c8defd5-91f2-4256-9f16-e7fa7546dec4") return StatusCode(StatusCodes.Status403Forbidden, new { message = "Bu kullanıcı silinemez" });

            AppUser? appUser = await _userManager.FindByIdAsync(id);
            if (appUser == null) return StatusCode(StatusCodes.Status404NotFound, new { message = "Kullanıcı bulunamadı" });

            IdentityResult result = await _userManager.DeleteAsync(appUser);
            if (!result.Succeeded)
            {
                string[] errors = result.Errors.Select(x => x.Description).ToArray();
                return StatusCode(StatusCodes.Status500InternalServerError, new { message = errors });
            }

            return Ok(new { message = "Kullnıcı silindi" });
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> ConfirmMail(string id)
        {
            AppUser appUser = await _userManager.FindByIdAsync(id);

            bool mailConfirm = await _userManager.IsEmailConfirmedAsync(appUser);

            appUser.EmailConfirmed = mailConfirm == true ? false : true;

            IdentityResult result = await _userManager.UpdateAsync(appUser);
            if (!result.Succeeded)
            {
                TempData["fail"] = result.Errors.Select(x => x.Description);
                return RedirectToAction(nameof(Index), "User", new { Area = "Admin" });
            }
            else
            {
                TempData["success"] = "Mail onay değişimi gerçekleşti";
                return RedirectToAction(nameof(Index), "User", new { Area = "Admin" });
            }
        }
    }
}
