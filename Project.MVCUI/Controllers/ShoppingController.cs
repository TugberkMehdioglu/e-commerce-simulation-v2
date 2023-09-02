using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Project.BLL.ManagerServices.Abstracts;
using Project.COMMON.Extensions;
using Project.COMMON.Tools;
using Project.ENTITIES.Enums;
using Project.ENTITIES.Models;
using Project.MVCUI.Areas.Admin.AdminViewModels;
using Project.MVCUI.Models.ShoppingTools;
using Project.MVCUI.ViewModels;
using Project.MVCUI.ViewModels.WrapperClasses;

namespace Project.MVCUI.Controllers
{
    [Route("[Controller]/[Action]")]
    public class ShoppingController : Controller
    {
        private readonly IProductManager _productManager;
        private readonly ICategoryManager _categoryManager;
        private readonly IAppUserManager _appUserManager;
        private readonly IOrderManager _orderManager;
        private readonly IOrderDetailManager _orderDetailManager;
        private readonly IAddressManager _addressManager;
        private readonly IMapper _mapper;

        public ShoppingController(IProductManager productManager, ICategoryManager categoryManager, IMapper mapper, IAppUserManager appUserManager, IOrderManager orderManager, IOrderDetailManager orderDetailManager, IAddressManager addressManager)
        {
            _productManager = productManager;
            _categoryManager = categoryManager;
            _mapper = mapper;
            _appUserManager = appUserManager;
            _orderManager = orderManager;
            _orderDetailManager = orderDetailManager;
            _addressManager = addressManager;
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
            else if (!string.IsNullOrEmpty(search) && search != "null")
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
            else if (!string.IsNullOrEmpty(search) && search != "null")
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
            if (sort != null && sort != "null") ViewBag.selectedSort = sort;

            if (sort == null || sort == "null") return query;
            else if (sort == "eyu") return query.OrderBy(x => x.CreatedDate);
            else if (sort == "edf") return query.OrderBy(x => x.Price);
            else if (sort == "eyf") return query.OrderByDescending(x => x.Price);
            else if (sort == "aaz") return query.OrderBy(x => x.Name);
            else return query.OrderByDescending(x => x.Name);
        }

        [Authorize]
        public async Task<IActionResult> ConfirmOrder()
        {
            string userName = User.Identity!.Name!;

            var (isSuccess, error, appUser) = await _appUserManager.GetUserWithProfileAndAddressAsync(userName);
            if (!isSuccess)
            {
                TempData["fail"] = error;
                return RedirectToAction(nameof(Index));
            }

            if (!await _addressManager.AnyAsync(x => x.AppUserProfileId == appUser!.Id))
            {
                TempData["fail"] = "Sipariş verebilmeniz için en az 1 adet adres girmiş olmalısınız";
                return RedirectToAction(nameof(CartPage));
            }

            OrderWrapper wrapper = new() { AppUser = _mapper.Map<AppUserViewModel>(appUser) };

            return View(wrapper);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize]
        public async Task<IActionResult> ConfirmOrder(OrderWrapper request)
        {
            ModelState.Remove("AppUser.PasswordHash");
            ModelState.Remove("AppUser.PhoneNumber");
            ModelState.Remove("AppUser.PasswordHashConfirm");
            ModelState.Remove("AppUser.UserName");
            ModelState.Remove("FullAddress");
            if (!ModelState.IsValid) return View(request);

            Cart? cart = HttpContext.Session.GetSession<Cart>("cart");
            if (cart == null)
            {
                TempData["fail"] = "Sepetinizde ürün bulunmamaktadır";
                return RedirectToAction(nameof(Index));
            }

            Order order = new()
            {
                TotalPrice = cart.TotalPrice,
                AppUserId = request.AppUser!.Id,
                AddressId = request.AddressId
            };

            var (isSuccess, error) = await _orderManager.AddAsync(order);
            if (!isSuccess)
            {
                TempData["fail"] = error;
                return RedirectToAction(nameof(Index));
            }

            List<string> orderedProductsForMailBody = new();

            foreach (CartItem item in cart.Basket)
            {
                OrderDetail orderDetail = new()
                {
                    OrderId = order.Id,
                    ProductId = item.ID,
                    Quantity = item.Amount,
                    SubTotal = item.SubTotal
                };

                orderedProductsForMailBody.Add(
                    @$"
                    <li style='margin-bottom: 20px; padding: 20px; border: 1px solid #e0e0e0; border-radius: 10px;'>
                    <strong>Ürün:</strong> {item.Name}
                    <br>
                    <strong>Fiyat:</strong> {item.Price.ToString("C2")}
                    <br>
                    <strong>Miktar:</strong> {item.Amount}
                    </li>
                    ");

                var (odSuccess, odError) = await _orderDetailManager.AddAsync(orderDetail);
                if (!odSuccess)
                {
                    TempData["fail"] = odError;
                    return RedirectToAction(nameof(Index));
                }

                Product? product = await _productManager.FindAsync(item.ID);

                product!.Stock -= item.Amount;
                var (pSuccess, pError) = await _productManager.UpdateAsync(product);
                if (!pSuccess)
                {
                    TempData["fail"] = pError;
                    return RedirectToAction(nameof(Index));
                }
            }

            string organizedProductsForMailBody = string.Join(null, orderedProductsForMailBody);
            string fullAddress = (await _addressManager.Where(x => x.Id == request.AddressId && x.Status != DataStatus.Deleted).Select(x => x.FullAddress).FirstOrDefaultAsync())!;

            string mailBody =
                @$"
      <div style='max-width: 800px; margin-top: 1%; padding: 20px; background-color: white; box-shadow: 0px 2px 5px rgba(0, 0, 0, 0.1); border-radius: 10px;'>
    <div style='background-color: #2c7be5; color: white; text-align: center; padding: 15px; border-radius: 10px 10px 10px 10px; font-family: sans-serif;'>
        <h1>TechByGamers</h1>
    </div>
    <div style='margin-top: 20px; padding: 20px; border: 1px solid #e0e0e0; border-radius: 10px;'>
      <div style='margin-bottom: 20px; padding: 20px; border: 1px solid #e0e0e0; border-radius: 10px;'>
        <h2 style='text-align: center;'>Fatura Bilgileri</h2>
        <p><strong>Müşteri Adı:</strong> {request.AppUser.AppUserProfile!.FullName}</p>
        <p><strong>Adres:</strong> {fullAddress}</p>
        <p><strong>Telefon:</strong> {request.AppUser.PhoneNumber}</p>
      </div>

        <h2 style='text-align: center;'>Sipariş Detayları</h2>
        <ul style='list-style-type: none; padding: 0; margin: 0;'>
            {organizedProductsForMailBody}
        </ul>
        <p style='margin-top: 20px; text-align: right; font-size: 20px;'><strong>Toplam Tutar:</strong> {order.TotalPrice.ToString("C2")}</p>
    </div>
</div>
    ";

            HttpContext.Session.Remove("cart");
            MailService.SendMailAsync(request.AppUser.Email, mailBody, "Siparişiniz için teşekkürler | TechByGamers");
            TempData["success"] = "Siparişiniz başarıyla alınmıştır, faturanız mail adresinize gönderildi";

            return RedirectToAction(nameof(Index));
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
