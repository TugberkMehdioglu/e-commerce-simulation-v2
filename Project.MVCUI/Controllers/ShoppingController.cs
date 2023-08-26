using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Project.BLL.ManagerServices.Abstracts;
using Project.COMMON.Extensions;
using Project.ENTITIES.Enums;
using Project.ENTITIES.Models;
using Project.MVCUI.Areas.Admin.AdminViewModels;
using Project.MVCUI.Models.ShoppingTools;
using Project.MVCUI.ViewModels.WrapperClasses;

namespace Project.MVCUI.Controllers
{
    [Route("[Controller]/[Action]")]
    public class ShoppingController : Controller
    {
        private readonly IProductManager _productManager;
        private readonly ICategoryManager _categoryManager;
        private readonly IMapper _mapper;

        public ShoppingController(IProductManager productManager, ICategoryManager categoryManager, IMapper mapper)
        {
            _productManager = productManager;
            _categoryManager = categoryManager;
            _mapper = mapper;
        }

        [Route("/")]
        [Route("/Home")]
        [HttpGet("{categoryID?}/{search?}/{selectSort?}/{pageNumber?}/{pageSize?}")]
        public async Task<IActionResult> Index(int? categoryID, string? search, string? selectSort, int pageNumber = 1, int pageSize = 9)
        {
            int totalItemsCount;
            if (categoryID.HasValue)
            {
                ViewBag.CategoryID = categoryID;
                totalItemsCount = await _productManager.GetActives().Where(x => x.CategoryId == categoryID).CountAsync();
            }
            else if (search != null)
            {
                ViewBag.search = search;
                totalItemsCount = await _productManager.GetActives().Where(x => x.Name.ToLower().Contains(search.ToLower())).CountAsync();
            }
            else totalItemsCount = await _productManager.GetActives().CountAsync();


            ShoppingListWrapper wrapper = new();
            IQueryable<Product> query;

            if (categoryID.HasValue)
            {
                query = _productManager.GetActives().Include(x => x.Category).Where(x => x.Category.Id == categoryID);

                query = SortProducts(query, selectSort);

                wrapper.Products = await query.Skip((pageNumber - 1) * pageSize).Take(pageSize).Select(x => new ProductViewModel()
                {
                    Id = x.Id,
                    Name = x.Name,
                    ImagePath = x.ImagePath,
                    Price = x.Price,
                    Stock = x.Stock,
                    Category = new CategoryViewModel() { Id = x.Category.Id, Name = x.Category.Name }
                }).ToListAsync();
            }
            else if (search != null)
            {
                query = _productManager.GetActives().Where(x => x.Name.ToLower().Contains(search.ToLower())).Include(x => x.Category);

                query = SortProducts(query, selectSort);

                wrapper.Products = await query.Skip((pageNumber - 1) * pageSize).Take(pageSize).Select(x => new ProductViewModel()
                {
                    Id = x.Id,
                    Name = x.Name,
                    ImagePath = x.ImagePath,
                    Price = x.Price,
                    Stock = x.Stock,
                    Category = new CategoryViewModel() { Id = x.Category.Id, Name = x.Category.Name }
                }).ToListAsync();
            }
            else
            {
                query = _productManager.GetActives().Include(x => x.Category);

                query = SortProducts(query, selectSort);

                wrapper.Products = await query.Skip((pageNumber - 1) * pageSize).Take(pageSize).Select(x => new ProductViewModel()
                {
                    Id = x.Id,
                    Name = x.Name,
                    ImagePath = x.ImagePath,
                    Price = x.Price,
                    Stock = x.Stock,
                    Category = new CategoryViewModel() { Id = x.Category.Id, Name = x.Category.Name }
                }).ToListAsync();
            }

            wrapper.Categories = await _categoryManager.GetActives().Select(x => new CategoryViewModel() { Id = x.Id, Name = x.Name }).ToListAsync();

            ViewBag.totalPagesCount = (int)Math.Ceiling((double)totalItemsCount / pageSize);
            ViewBag.pageNumber = pageNumber;
            ViewBag.totalItemsCount = totalItemsCount;

            return View(wrapper);
        }

        public IQueryable<Product> SortProducts(IQueryable<Product> query, string? sort)
        {
            if(sort!=null) ViewBag.selectedSort = sort;

            if (sort == null) return query;
            else if (sort == "eyu") return query.OrderBy(x => x.CreatedDate);
            else if (sort == "edf") return query.OrderBy(x => x.Price);
            else if (sort == "eyf") return query.OrderByDescending(x => x.Price);
            else if (sort == "aaz") return query.OrderBy(x => x.Name);
            else return query.OrderByDescending(x => x.Name);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> ProductDetail(int id)
        {
            var (isSuccess, error, product) = await _productManager.GetActiveProductWithAttributeAndCategoryAsync(id);
            if (!isSuccess)
            {
                TempData["fail"] = error;
                return RedirectToAction(nameof(Index));
            }

            ProductViewModel productViewModel = _mapper.Map<ProductViewModel>(product);

            return View(productViewModel);
        }

        public IActionResult CartPage()
        {
            Cart? basket = HttpContext.Session.GetSession<Cart>("cart");
            if (basket == null)
            {
                TempData["fail"] = "Sepetinizde ürün bulunmamaktadır";
                return Redirect(nameof(Index));
            }

            return View(basket);
        }


        [HttpGet("{id}/{categoryID?}/{pageNumber?}/{from?}/{quantity?}")]
        public async Task<IActionResult> AddToCart(int id, int? categoryID, int? pageNumber, string? from, short? quantity)
        {
            Product? toBeAdded = await _productManager.FindAsync(id);
            if (toBeAdded == null) return RedirectToAction(nameof(Index), "Product", new { Area = "" });

            Cart? basket = HttpContext.Session.GetSession<Cart>("cart");
            if (basket == null) basket = new Cart();

            CartItem cartItem = new()
            {
                ID = toBeAdded.Id,
                Name = toBeAdded.Name,
                Price = toBeAdded.Price,
                ImagePath = toBeAdded.ImagePath
            };
            if (quantity.HasValue && quantity > 0 && quantity <= toBeAdded.Stock) cartItem.Amount = quantity.Value;

            basket.AddToBasket(cartItem);
            HttpContext.Session.SetSession("cart", basket);

            TempData["success"] = "Ürün sepete eklendi";
            if (from != null && from == "cart") return RedirectToAction(nameof(CartPage));
            else if (from != null && from == "ProductDetail") return RedirectToAction(nameof(ProductDetail), new { id });
            else return RedirectToAction(nameof(Index), new { categoryID, pageNumber });
        }

        [HttpGet("{id}")]
        public IActionResult DeleteFromCart(int id)
        {
            Cart? basket = HttpContext.Session.GetSession<Cart>("cart");
            if (basket == null) return RedirectToAction(nameof(Index));

            if (!basket.Basket.Any(x => x.ID == id)) return RedirectToAction(nameof(Index));

            basket.RemoveFromBasket(id);
            if (!basket.Basket.Any())
            {
                HttpContext.Session.Remove("cart");
                TempData["fail"] = "Sepetinizde ürün bulunmamaktadır";
                return RedirectToAction(nameof(Index));
            }

            HttpContext.Session.SetSession("cart", basket);

            TempData["success"] = "Ürün sepetten silindi";
            return RedirectToAction(nameof(CartPage));
        }

        [HttpGet("{id}")]
        public IActionResult DeleteProductWithAllAmountFromCart(int id)
        {
            Cart? basket = HttpContext.Session.GetSession<Cart>("cart");
            if (basket == null) return RedirectToAction(nameof(Index));

            if (!basket.Basket.Any(x => x.ID == id)) return RedirectToAction(nameof(Index));

            basket.RemoveItemWithAllAmountFromBasket(id);
            if (!basket.Basket.Any())
            {
                HttpContext.Session.Remove("cart");
                TempData["fail"] = "Sepetinizde ürün bulunmamaktadır";
                return RedirectToAction(nameof(Index));
            }

            HttpContext.Session.SetSession("cart", basket);

            TempData["success"] = "Ürün sepetten silindi";
            return RedirectToAction(nameof(CartPage));
        }
    }
}
