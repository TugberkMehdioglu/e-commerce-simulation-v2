using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Project.BLL.ManagerServices.Abstracts;
using Project.ENTITIES.Enums;
using Project.ENTITIES.Models;
using Project.MVCUI.Areas.Admin.AdminViewModels;
using Project.MVCUI.Areas.Admin.AdminViewModels.AdminWrapperClasses;
using Project.MVCUI.Extensions;

namespace Project.MVCUI.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Route("[Area]/[Controller]/[Action]")]
    [Authorize(Roles ="Admin")]
    public class CategoryController : Controller
    {
        private readonly ICategoryManager _categoryManager;
        private readonly IMapper _mapper;

        public CategoryController(ICategoryManager categoryManager, IMapper mapper)
        {
            _categoryManager = categoryManager;
            _mapper = mapper;
        }

        [HttpGet("{id?}")]
        public async Task<IActionResult> Index(int? id)
        {
            CategoryWrapper wrapper = id == null ? new CategoryWrapper()
            {
                Categories = await _categoryManager.GetActives().Select(x => new CategoryViewModel()
                {
                    Id = x.Id,
                    Name = x.Name,
                    Description = x.Description
                }).ToListAsync()
            }
            : new CategoryWrapper()
            {
                Category = await _categoryManager.Where(x => x.Id == id && x.Status != DataStatus.Deleted).Select(x => new CategoryViewModel()
                {
                    Id = x.Id,
                    Name = x.Name,
                    Description = x.Description
                }).FirstOrDefaultAsync()
            };
            //Todo: Buranın view'undaki ürünleri action'ını oluştur ProductController'da.
            return View(wrapper);
        }

        public IActionResult AddCategory()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> AddCategory(CategoryViewModel request)
        {
            if (!ModelState.IsValid) return View();

            if (await _categoryManager.AnyAsync(x => x.Name == request.Name && x.Status != DataStatus.Deleted))
            {
                ModelState.AddModelErrorWithOutKey("Oluşturmak istediğiniz kategori adı zaten mevcut");
                return View();
            }

            var (isSuccess, error) = await _categoryManager.AddAsync(_mapper.Map<Category>(request));
            if (!isSuccess)
            {
                ModelState.AddModelErrorWithOutKey(error!);
                return View();
            }

            TempData["success"] = "Kategori oluşturuldu";
            return RedirectToAction(nameof(Index), "Category", new { Area = "Admin" });
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> UpdateCategory(int id)
        {
            Category? category = await _categoryManager.FindAsync(id);
            if (category == null) return RedirectToAction(nameof(Index), "Category", new { Area = "Admin" });

            CategoryViewModel categoryViewModel = _mapper.Map<CategoryViewModel>(category);
            categoryViewModel.FormerName = category.Name;

            return View(categoryViewModel);
        }

        [HttpPost("{id}")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> UpdateCategory(CategoryViewModel request)
        {
            if (!ModelState.IsValid) return View();

            if (request.FormerName != request.Name && await _categoryManager.AnyAsync(x => x.Name == request.Name && x.Status != DataStatus.Deleted))  
            {
                ModelState.AddModelErrorWithOutKey("Güncellemek istediğiniz kategori adı zaten mevcut");
                return View();
            }

            var (isSuccess, error) = await _categoryManager.UpdateAsync(_mapper.Map<Category>(request));
            if (!isSuccess)
            {
                ModelState.AddModelErrorWithOutKey(error!);
                return View();
            }

            TempData["success"] = "Kategori güncellendi";
            return RedirectToAction(nameof(Index), "Category", new { Area = "Admin" });
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> DeleteCategory(int id)
        {
            Category? category = await _categoryManager.FindAsync(id);
            if (category == null) return StatusCode(StatusCodes.Status404NotFound, new { message = "Kategori bulunamadı" });

            var (isSuccess, error) = await _categoryManager.DeleteAsync(category);
            if (!isSuccess) return StatusCode(StatusCodes.Status500InternalServerError, new { message = error });

            return Ok(new { message = "Kategori silindi" });
        }
    }
}
