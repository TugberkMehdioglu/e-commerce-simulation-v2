using AutoMapper;
using Project.ENTITIES.Models;
using Project.MVCUI.ViewModels;

namespace Project.MVCUI.Mapping
{
    public class ViewModelMapping : Profile
    {
        public ViewModelMapping()
        {
            CreateMap<AppUser, AppUserViewModel>().ReverseMap();
            CreateMap<AppUserProfile, AppUserProfileViewModel>().ReverseMap();
        }
    }
}
