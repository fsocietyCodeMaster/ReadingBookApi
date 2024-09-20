using AutoMapper;
using ReadingBookApi.Model;

namespace ReadingBookApi.Customized.AutoMapper
{
    public class ReviewMapper : Profile
    {
        public ReviewMapper()
        {
            CreateMap<ReviewVM, T_Review>();
            CreateMap<T_Review, ReviewVM>();

        }
    }
}
