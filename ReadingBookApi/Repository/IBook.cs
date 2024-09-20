using Microsoft.AspNetCore.JsonPatch;
using ReadingBookApi.Model;

namespace ReadingBookApi.Repository
{
    public interface IBook
    {
        Task<ResponseVM> AddBook(BookDetailVM book);
        Task<ResponseVM> GetAllByPage(int page , int size);
        Task<ResponseVM> Get(Guid id,int page ,int size);
        Task<T_Book> GetBookId(Guid id);
        Task<ResponseVM> Delete(Guid id);
        Task<ResponseVM> Update(Guid id ,BookDetailVM book);
        Task<ResponseVM> UpdatePartial(Guid id , JsonPatchDocument<BookDetailVM> book);
        Task<ResponseVM> GetBySearch(string filter);
    }
}
