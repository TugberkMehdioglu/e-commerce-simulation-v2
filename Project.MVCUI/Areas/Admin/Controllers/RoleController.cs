using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Project.MVCUI.Areas.Admin.AdminViewModels;
using Project.MVCUI.Extensions;

namespace Project.MVCUI.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Route("[Area]/[Controller]/[Action]")]
    [Authorize(Roles ="Admin")]
    public class RoleController : Controller
    {
        private readonly RoleManager<IdentityRole> _roleManager;

        public RoleController(RoleManager<IdentityRole> roleManager)
        {
            _roleManager = roleManager;
        }

        public async Task<IActionResult> Index()
        {
            List<RoleViewModel> roles = await _roleManager.Roles.Select(x => new RoleViewModel() { Id = x.Id, Name = x.Name }).ToListAsync();

            if (TempData["error"] != null) ViewBag.error = TempData["error"]!.ToString();

            return View(roles);
        }

        public IActionResult AddRole()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> AddRole(RoleViewModel request)
        {
            if (!ModelState.IsValid) return View();

            IdentityResult result = await _roleManager.CreateAsync(new() { Name = request.Name });
            if (!result.Succeeded)
            {
                ModelState.AddModelErrorListWithOutKey(result.Errors);
                return View();
            }

            TempData["success"] = "Rol oluşturuldu";
            return RedirectToAction(nameof(Index), "Role", new { Area = "Admin" });
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> UpdateRole(string id)
        {
            IdentityRole? role = await _roleManager.FindByIdAsync(id);
            if (role == null)
            {
                TempData["error"] = "Güncellenecek rol bulunamadı";
                return RedirectToAction(nameof(Index), "Role", new { Area = "Admin" });
            }
            

            return View(new RoleViewModel() { Id = role.Id, Name = role.Name });
        }

        [HttpPost("{id}")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> UpdateRole(RoleViewModel request)
        {
            if (!ModelState.IsValid) return View();

            IdentityRole? role = await _roleManager.FindByIdAsync(request.Id);
            if (role == null)
            {
                TempData["error"] = "Güncellenecek rol bulunamadı";
                return RedirectToAction(nameof(Index), "Role", new { Area = "Admin" });
            }
            role.Name = request.Name;

            IdentityResult result = await _roleManager.UpdateAsync(role);
            if (!result.Succeeded)
            {
                ModelState.AddModelErrorListWithOutKey(result.Errors);
                return View();
            }

            TempData["success"] = "Rol güncellendi";
            return RedirectToAction(nameof(Index), "Role", new { Area = "Admin" });
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> DeleteRole(string id)
        {
            IdentityRole role = await _roleManager.FindByIdAsync(id);
            if (role == null) return StatusCode(StatusCodes.Status404NotFound, new { message = "Rol bulunamadı" });

            IdentityResult result = await _roleManager.DeleteAsync(role);
            if (!result.Succeeded)
            {
                string[] errors = result.Errors.Select(x => x.Description).ToArray();
                return StatusCode(StatusCodes.Status500InternalServerError, new { message = errors });
            }

            return Ok(new { message = "Rol silindi" });
        }
    }
}
