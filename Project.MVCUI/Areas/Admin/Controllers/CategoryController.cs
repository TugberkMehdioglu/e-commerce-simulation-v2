using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Project.BLL.ManagerServices.Abstracts;
using Project.ENTITIES.Enums;
using Project.ENTITIES.Models;
using Project.MVCUI.Areas.Admin.AdminViewModels;
using Project.MVCUI.Areas.Admin.AdminViewModels.AdminWrapperClasses;

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

            return View(wrapper);
        }
    }
}
