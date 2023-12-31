﻿using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using NuGet.Protocol;
using Project.BLL.ManagerServices.Abstracts;
using Project.ENTITIES.Enums;
using Project.ENTITIES.Models;
using Project.MVCUI.Extensions;
using Project.MVCUI.ViewModels;
using System.Collections.Specialized;
using System.Net;

namespace Project.MVCUI.Controllers
{
    [Authorize]
    [Route("[Controller]/[Action]")]
    public class AddressController : Controller
    {
        private readonly IAddressManager _addressManager;
        private readonly IMapper _mapper;
        private readonly IAppUserManager _appUserManager;

        public AddressController(IAddressManager addressManager, IMapper mapper, IAppUserManager appUserManager)
        {
            _addressManager = addressManager;
            _mapper = mapper;
            _appUserManager = appUserManager;
        }

        public async Task<IActionResult> Addresses()
        {
            string? appUserProfileId = await _appUserManager.Where(x => x.UserName == User.Identity!.Name && x.Status != DataStatus.Deleted).Select(x => x.Id).FirstOrDefaultAsync();

            List<Address>? addresses = await _addressManager.Where(x => x.Status != DataStatus.Deleted && x.AppUserProfileId == appUserProfileId).ToListAsync();
            if (addresses == null) return RedirectToAction(nameof(AddAddress));

            List<AddressViewModel> viewModels = _mapper.Map<List<AddressViewModel>>(addresses);

            //For UpdateAddress action's error
            if (TempData["error"] != null) ModelState.AddModelErrorWithOutKey(TempData["error"]!.ToString()!);

            return View(viewModels);
        }

        [HttpGet("{country}")]
        public JsonResult GetCities(string country)
        {
            List<string> cities = new List<string>();

            if(country == "turkey")
            {
                cities = new() { "" ,"Adana", "Adıyaman", "Afyonkarahisar", "Ağrı", "Aksaray", "Amasya", "Ankara", "Antalya", "Ardahan", "Artvin", "Aydın", "Balıkesir", "Bartın", "Batman", "Bayburt", "Bilecik", "Bingöl", "Bitlis", "Bolu", "Burdur", "Bursa", "Çanakkale", "Çankırı", "Çorum", "Denizli", "Diyarbakır", "Düzce", "Edirne", "Elazığ", "Erzincan", "Erzurum", "Eskişehir", "Gaziantep", "Giresun", "Gümüşhane", "Hakkâri", "Hatay", "Iğdır", "Isparta", "İstanbul", "İzmir", "Kahramanmaraş", "Karabük", "Karaman", "Kars", "Kastamonu", "Kayseri", "Kırıkkale", "Kırklareli", "Kırşehir", "Kilis", "Kocaeli", "Konya", "Kütahya", "Malatya", "Manisa", "Mardin", "Mersin", "Muğla", "Muş", "Nevşehir", "Niğde", "Ordu", "Osmaniye", "Rize", "Sakarya", "Samsun", "Siirt", "Sinop", "Sivas", "Şanlıurfa", "Şırnak", "Tekirdağ", "Tokat", "Trabzon", "Tunceli", "Uşak", "Van", "Yalova", "Yozgat", "Zonguldak" };
            }
            else if(country == "italy")
            {
                cities = new() { "" ,"Roma", "Milano", "Napoli", "Torino", "Palermo", "Cenova", "Bologna", "Floransa", "Venedik", "Verona", "Bari", "Catania", "Genoa", "Pisa", "Siena", "Modena", "Ravenna", "Parma", "Bergamo", "Perugia" };
            };

            return Json(cities);
        }

        public IActionResult AddAddress()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> AddAddress(AddressViewModel request)
        {
            if (!ModelState.IsValid) return View(request);

            request.AppUserProfileId = await _appUserManager.Where(x => x.UserName == User.Identity!.Name).Select(x => x.Id).FirstOrDefaultAsync();

            var (isSuccess, error) = await _addressManager.AddAsync(_mapper.Map<Address>(request));
            if (!isSuccess)
            {
                ModelState.AddModelErrorWithOutKey(error!);
                return View(request);
            }

            TempData["success"] = "Adres oluşturuldu";
            return RedirectToAction(nameof(Addresses));
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> UpdateAddress(int id)
        {
            Address? address = await _addressManager.FindAsync(id);
            if (address == null) return RedirectToAction(nameof(Addresses));

            return View(_mapper.Map<AddressViewModel>(address));
        }

        [HttpPost("{id}")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> UpdateAddress(AddressViewModel request)
        {
            if (!ModelState.IsValid) return View();

            Address address = _mapper.Map<Address>(request);

            var (isSuccess, error) = await _addressManager.UpdateAsync(address);
            if (!isSuccess)
            {
                TempData["error"] = error;
                return RedirectToAction(nameof(Addresses));
            }

            TempData["success"] = "Adres güncellendi";
            return RedirectToAction(nameof(Addresses));
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> DeleteAddress(int id)
        {
            Address? address = await _addressManager.FindAsync(id);
            if (address == null) return StatusCode(StatusCodes.Status404NotFound, new { message = "Adres bulunamadı" });

            var (isSuccess, error) = await _addressManager.DeleteAsync(address);
            if (!isSuccess) return StatusCode(StatusCodes.Status500InternalServerError, new { message = error });

            return Ok("Adres silindi");
        }
    }
}
