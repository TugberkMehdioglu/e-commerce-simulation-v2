using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Project.BLL.ManagerServices.Abstracts;
using Project.ENTITIES.Enums;
using Project.ENTITIES.Models;
using Project.MVCUI.ViewModels;

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
            string? appUserProfileId = await _appUserManager.Where(x => x.UserName == User.Identity!.Name).Select(x => x.Id).FirstOrDefaultAsync();

            List<Address>? addresses = await _addressManager.Where(x => x.Status != DataStatus.Deleted && x.AppUserProfileId == appUserProfileId).ToListAsync();
            if (addresses == null) return RedirectToAction("AddAddress");//Todo:Add this action

            List<AddressViewModel> viewModels = _mapper.Map<List<AddressViewModel>>(addresses);

            return View(viewModels);
        }
    }
}
