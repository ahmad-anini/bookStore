using AutoMapper;
using bookStore.Models;
using bookStore.ViewModels;

namespace bookStore.Profiles
{
    public class UserProfile : Profile
    {
        public UserProfile()
        {
            CreateMap<ApplicationUser, ApplicationUserVM>()
                .ForMember(dest => dest.Roles, opt => opt.Ignore());

            CreateMap<ApplicationUserCreateVM, ApplicationUser>();
        }
    }
}
