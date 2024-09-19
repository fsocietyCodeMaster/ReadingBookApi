using AutoMapper;
using ReadingBookApi.Model;

namespace ReadingBookApi.Customized.AutoMapper
{
    public class BookMapp : Profile
    {
        public BookMapp()
        {
            CreateMap<BookVM,T_Book>();
            CreateMap<T_Book, BookVM>();
        }
    }
}
