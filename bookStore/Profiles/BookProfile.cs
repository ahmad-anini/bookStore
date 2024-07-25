using AutoMapper;
using bookStore.Models;
using bookStore.ViewModels;

namespace bookStore.Profiles
{
    public class BookProfile : Profile
    {
        public BookProfile()
        {
            CreateMap<Book, BookVM>()
                .ForMember(dest => dest.Categories,
                     opt => opt.MapFrom(src => src.Categories.Select(c => c.Category.Name).ToList()))
                .ForMember(dest => dest.Auther,
                      opt => opt.MapFrom(src => src.Author.Name));

            CreateMap<BookFormVM, Book>()
                .ForMember(dest => dest.Categories,
                    opt => opt.MapFrom(src => src.SelectedCategories
                        .Select(id => new BookCategory { CategoryId = id }).ToList()));

        }
    }
}
