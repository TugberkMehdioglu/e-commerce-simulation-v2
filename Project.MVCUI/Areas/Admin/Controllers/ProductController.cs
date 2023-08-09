using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.FileProviders;
using Project.BLL.ManagerServices.Abstracts;
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
        public ProductController(IProductManager productManager, IMapper mapper, IFileProvider fileProvider)
        {
            _productManager = productManager;
            _mapper = mapper;
            _fileProvider = fileProvider;
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
    }
}
