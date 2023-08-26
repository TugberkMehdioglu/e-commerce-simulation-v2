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
        private readonly IProductAttributeManager _productAttributeManager;
        private readonly IMapper _mapper;
        private readonly IFileProvider _fileProvider;
        private readonly ICategoryManager _categoryManager;
        public ProductController(IProductManager productManager, IMapper mapper, IFileProvider fileProvider, ICategoryManager categoryManager, IProductAttributeManager productAttributeManager)
        {
            _productManager = productManager;
            _mapper = mapper;
            _fileProvider = fileProvider;
            _categoryManager = categoryManager;
            _productAttributeManager = productAttributeManager;
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

        [HttpGet("{item?}")]
        public async Task<IActionResult> SearchProducts(string? item)
        {
            var (isSuccess, error, products) = string.IsNullOrWhiteSpace(item) ? await _productManager.GetActiveProductsWithCategory() : await _productManager.GetActiveProductsWithCategory(item);

            List<ProductViewModel> productViewModels = _mapper.Map<List<ProductViewModel>>(products);

            if (!isSuccess)
            {
                ModelState.AddModelErrorWithOutKey(error!);
                return View(nameof(Index), productViewModels);
            }
            if(products!.Count == 0)
            {
                var (success, errorr, allProducts) = await _productManager.GetActiveProductsWithCategory();
                if (!success)
                {
                    ModelState.AddModelErrorWithOutKey(errorr!);
                    return View(nameof(Index), productViewModels);
                }

                List<ProductViewModel> productViewModels2 = _mapper.Map<List<ProductViewModel>>(allProducts);
                ViewBag.alert = "Bu isimde bir ürün bulunamadı";
                return View(nameof(Index), productViewModels2);
            }
            
            return View(nameof(Index), productViewModels);
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

            return View(productViewModel);
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

        [HttpGet("{id}")]
        public async Task<IActionResult> Edit(int id)
        {
            var (isSuccess, error, product) = await _productManager.GetActiveProductWithAttributeAndCategoryAsync(id);
            if (!isSuccess)
            {
                ModelState.AddModelErrorWithOutKey(error!);
                return RedirectToAction(nameof(Index), "Product", new { Area = "Admin" });
            }

            ProductViewModel productViewModel = _mapper.Map<ProductViewModel>(product);
            productViewModel.FormerName = product!.Name;
            productViewModel.FormerImagePath = product.ImagePath;

            await AddCategoriesSelectListForSelectedProduct(productViewModel.CategoryID);

            return View(productViewModel);
        }

        [HttpPost("{id}")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(ProductViewModel request)
        {
            if (!ModelState.IsValid)
            {
                await AddCategoriesSelectListForSelectedProduct(request.CategoryID);
                request.ImagePath = request.FormerImagePath;
                return View(request);
            }

            if (request.FormerName != request.Name && await _productManager.AnyAsync(x => x.Name == request.Name && x.Status != DataStatus.Deleted))
            {
                ModelState.AddModelErrorWithOutKey("Güncellemek istediğiniz ürün adı zaten mevcut");
                await AddCategoriesSelectListForSelectedProduct(request.CategoryID);
                request.ImagePath = request.FormerImagePath;
                return View(request);
            }

            Product product = _mapper.Map<Product>(request);

            if (request.Image != null && request.Image.Length > 0)
            {
                string? result = ImageUploader.UploadImageToProduct(request.Image, _fileProvider, out string? entityImagePath);
                if (result != null)
                {
                    ModelState.AddModelErrorWithOutKey(result);
                    await AddCategoriesSelectListForSelectedProduct(request.CategoryID);
                    request.ImagePath = request.FormerImagePath;
                    return View(request);
                }

                product.ImagePath = entityImagePath;
            }
            else product.ImagePath = request.FormerImagePath;

            var (isSuccess, error) = await _productManager.UpdateAsync(product);
            if (!isSuccess)
            {
                ModelState.AddModelErrorWithOutKey(error!);
                await AddCategoriesSelectListForSelectedProduct(request.CategoryID);
                request.ImagePath = request.FormerImagePath;
                return View(request);
            }

            List<ProductAttribute> productAttributes = _mapper.Map<List<ProductAttribute>>(request.ProductAttributes);

            bool success = false;
            string? attributeError = null;
            for (int i = 0; i < request.ProductAttributes!.Count; i++)
            {
                if (request.ProductAttributes[i].Id.HasValue)
                {
                    (success, attributeError) = await _productAttributeManager.UpdateAsync(productAttributes[i]);
                    if (!success)
                    {
                        ModelState.AddModelErrorWithOutKey(attributeError!);
                        await AddCategoriesSelectListForSelectedProduct(request.CategoryID);
                        request.ImagePath = request.FormerImagePath;
                        return View(request);
                    }
                }

                else
                {
                    (success, attributeError) = await _productAttributeManager.AddAsync(productAttributes[i]);
                    if (!success)
                    {
                        ModelState.AddModelErrorWithOutKey(attributeError!);
                        await AddCategoriesSelectListForSelectedProduct(request.CategoryID);
                        request.ImagePath = request.FormerImagePath;
                        return View(request);
                    }
                }
                    
            }

            TempData["success"] = "Ürün güncellendi";
            return RedirectToAction(nameof(Index), "Product", new { Area = "Admin" });
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

        public async Task AddCategoriesSelectListForSelectedProduct(int selectedCategoryId)
        {
            List<CategoryViewModel> categories = await _categoryManager.GetActives().Select(x => new CategoryViewModel()
            {
                Id = x.Id,
                Name = x.Name
            }).ToListAsync();

            TempData["categorySelectList"] = new SelectList(categories, nameof(CategoryViewModel.Id), nameof(CategoryViewModel.Name), selectedCategoryId);
        }

        [HttpGet("{selectedAttributeId}")]
        public async Task<ObjectResult> DeleteAttribute(int selectedAttributeId)
        {
            ProductAttribute? productAttribute = await _productAttributeManager.FindAsync(selectedAttributeId);
            if (productAttribute == null) return StatusCode(StatusCodes.Status404NotFound, new { message = "Ürün özelliği bulunamadı" });

            var (isSuccess, error) = await _productAttributeManager.DeleteAsync(productAttribute);
            if (!isSuccess) return StatusCode(StatusCodes.Status500InternalServerError, new { message = error });

            return Ok(new { message = "Ürün özelliği silindi" });
        }
    }
}
