using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.FileProviders;
using Project.BLL.ManagerServices.Abstracts;
using Project.COMMON.Tools;
using Project.ENTITIES.Enums;
using Project.ENTITIES.Models;
using Project.MVCUI.Areas.Admin.AdminViewModels;
using Project.MVCUI.Extensions;

namespace Project.MVCUI.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Route("[Area]/[Controller]/[Action]")]
    [Authorize(Roles ="Admin")]
    public class ProductController : Controller
    {
        private readonly IProductManager _productManager;
        private readonly IMapper _mapper;
        private readonly IFileProvider _fileProvider;
        private readonly ICategoryManager _categoryManager;
        public ProductController(IProductManager productManager, IMapper mapper, IFileProvider fileProvider, ICategoryManager categoryManager)
        {
            _productManager = productManager;
            _mapper = mapper;
            _fileProvider = fileProvider;
            _categoryManager = categoryManager;
        }

        [HttpGet("{categoryId?}")]
        public async Task<IActionResult> Index(int? categoryId)
        {
            var (isSuccess, error, products) = categoryId != null ? await _productManager.GetCategoriesActiveProductsAsync(categoryId.Value) : await _productManager.GetActiveProductsWithCategory();

            if (!isSuccess)
            {
                ModelState.AddModelErrorWithOutKey(error!);
                return View();
            }

            List<ProductViewModel> productViewModels = _mapper.Map<List<ProductViewModel>>(products);

            return View(productViewModels);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Details(int id)
        {
            var (isSuccess, error, product) = await _productManager.GetActiveProductWithAttributeAndCategoryAsync(id);
            if (!isSuccess)
            {
                TempData["fail"] = error;
                return RedirectToAction(nameof(Index), "Product", new { Area = "Admin" });
            }

            ProductViewModel productViewModel = _mapper.Map<ProductViewModel>(product);
            //Todo: Buranın view'sunda anakart yazan yerin yönlendirmesini sipariş sayfasında o kategori seçilmiş şekilde yönlendir
            return View(productViewModel);
        }

        public async Task AddCategoriesSelectListForProduct()
        {
            List<CategoryViewModel> categories = await _categoryManager.GetActives().Select(x => new CategoryViewModel()
            {
                Id = x.Id,
                Name = x.Name
            }).ToListAsync();

            TempData["categorySelectList"] = new SelectList(categories, nameof(CategoryViewModel.Id), nameof(CategoryViewModel.Name));
        }

        public async Task<IActionResult> Create()
        {
            await AddCategoriesSelectListForProduct();

            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(ProductViewModel request)
        {
            if (!ModelState.IsValid)
            {
                await AddCategoriesSelectListForProduct();
                return View();
            }
            else if (await _productManager.AnyAsync(x => x.Name.ToLower() == request.Name.ToLower() && x.Status != DataStatus.Deleted))
            {
                await AddCategoriesSelectListForProduct();
                ModelState.AddModelErrorWithOutKey("Oluşturmaya çalıştığınız ürün zaten bulunmaktadır");
                return View();
            }

            Product product = _mapper.Map<Product>(request);

            if (request.Image != null && request.Image.Length > 0)
            {
                string? result = ImageUploader.UploadImageToProduct(request.Image, _fileProvider, out string? entityImagePath);
                if(result != null)
                {
                    ModelState.AddModelErrorWithOutKey(result);
                    await AddCategoriesSelectListForProduct();
                    return View();
                }

                product.ImagePath = entityImagePath;
            }

            var (isSuccess, error) = await _productManager.AddAsync(product);
            if (!isSuccess)
            {
                ModelState.AddModelErrorWithOutKey(error!);
                await AddCategoriesSelectListForProduct();
                return View();
            }

            TempData["success"] = "Ürün başarıyla eklendi";
            return RedirectToAction(nameof(Index), "Product", new { Area = "Admin" });
        }
    }
}
