using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Project.BLL.ManagerServices.Abstracts;
using Project.ENTITIES.Enums;
using Project.MVCUI.Areas.Admin.AdminViewModels;
using Project.MVCUI.ViewModels.WrapperClasses;

namespace Project.MVCUI.Controllers
{
    [Route("[Controller]/[Action]")]
    public class ShoppingController : Controller
    {
        private readonly IProductManager _productManager;
        private readonly ICategoryManager _categoryManager;

        public ShoppingController(IProductManager productManager, ICategoryManager categoryManager)
        {
            _productManager = productManager;
            _categoryManager = categoryManager;
        }

        [Route("/")]
        [Route("/Home")]
        [HttpGet("{categoryID?}/{pageNumber?}/{pageSize?}")]
        public async Task<IActionResult> Index(int? categoryID, int pageNumber = 1, int pageSize = 9)
        {
            int totalItemsCount;
            if (categoryID.HasValue)
            {
                ViewBag.CategoryID = categoryID;
                totalItemsCount = await _productManager.GetActives().Where(x => x.CategoryId == categoryID).CountAsync();
            }
            else totalItemsCount = await _productManager.GetActives().CountAsync();

            ShoppingListWrapper wrapper = new()
            {
                Products = categoryID.HasValue ? await _productManager.GetActives().Include(x => x.Category).Where(x => x.Category.Id == categoryID).Skip((pageNumber - 1) * pageSize).Take(pageSize).Select(x => new ProductViewModel()
                {
                    Id = x.Id,
                    Name = x.Name,
                    ImagePath = x.ImagePath,
                    Price = x.Price,
                    Stock = x.Stock,
                    Category = new CategoryViewModel() { Id = x.Category.Id, Name = x.Category.Name }
                }).ToListAsync()

                : await _productManager.GetActives().Include(x => x.Category).Skip((pageNumber - 1) * pageSize).Take(pageSize).Select(x => new ProductViewModel()
                {
                    Id = x.Id,
                    Name = x.Name,
                    ImagePath = x.ImagePath,
                    Price = x.Price,
                    Stock = x.Stock,
                    Category = new CategoryViewModel() { Id = x.Category.Id, Name = x.Category.Name }
                }).ToListAsync()
            };

            wrapper.Categories = await _categoryManager.GetActives().Select(x => new CategoryViewModel() { Id = x.Id, Name = x.Name }).ToListAsync();
            ViewBag.totalPagesCount = (int)Math.Ceiling((double)totalItemsCount / pageSize);
            ViewBag.pageNumber = pageNumber;

            return View(wrapper);
        }
    }
}
