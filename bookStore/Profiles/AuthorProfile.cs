using AutoMapper;
using bookStore.Models;
using bookStore.ViewModels;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace bookStore.Profiles
{
    public class AuthorProfile : Profile
    {
        public AuthorProfile()
        {
            CreateMap<Author, AuthorVM>();

            CreateMap<AuthorFormVM, Author>().ReverseMap();

            CreateMap<Author, SelectListItem>()
                .ForMember(dest => dest.Value, opt => opt.MapFrom(src => src.Id.ToString()))
                .ForMember(dest => dest.Text, opt => opt.MapFrom(src => src.Name));

        }
    }
}
