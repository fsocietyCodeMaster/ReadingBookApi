using AutoMapper;
using ReadingBookApi.Model;

namespace ReadingBookApi.Customized.AutoMapper
{
    public class BookMapp : Profile
    {
        public BookMapp()
        {
            CreateMap<BookDetailVM, T_Book>();
            CreateMap<T_Book, BookDetailVM>().ForMember(dest => dest.reviews, opt => opt.MapFrom(src => src.review_Ratings));
            CreateMap<T_Book, BookSummaryVM>();
            CreateMap<T_Book, BookAdminDetailVM>();
            CreateMap<BookAdminDetailVM, T_Book>();

        }
    }
}
