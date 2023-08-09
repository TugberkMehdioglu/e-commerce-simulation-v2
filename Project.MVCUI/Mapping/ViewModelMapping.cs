using AutoMapper;
using Project.ENTITIES.Models;
using Project.MVCUI.Areas.Admin.AdminViewModels;
using Project.MVCUI.ViewModels;

namespace Project.MVCUI.Mapping
{
    public class ViewModelMapping : Profile
    {
        public ViewModelMapping()
        {
            CreateMap<AppUser, AppUserViewModel>().ReverseMap();
            CreateMap<AppUserProfile, AppUserProfileViewModel>().ReverseMap();
            CreateMap<Address, AddressViewModel>().ReverseMap();
            CreateMap<Category, CategoryViewModel>().ReverseMap();
            CreateMap<Product, ProductViewModel>().ReverseMap();
            CreateMap<ProductAttribute, ProductAttributeViewModel>().ReverseMap();
        }
    }
}
