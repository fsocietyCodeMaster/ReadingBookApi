using Microsoft.AspNetCore.JsonPatch;
using ReadingBookApi.Model;

namespace ReadingBookApi.Repository
{
    public interface IBook
    {
        Task<ResponseVM> AddBook(BookAdminDetailVM book);
        Task<ResponseVM> GetAllByPage(int page, int size);
        Task<ResponseVM> Get(Guid id, int page, int size);
        Task<T_Book> GetBookId(Guid id);
        Task<ResponseVM> Delete(Guid id);
        Task<ResponseVM> Update(Guid id, BookAdminDetailVM book);
        Task<ResponseVM> UpdatePartial(Guid id, JsonPatchDocument<BookAdminDetailVM> book);
        Task<ResponseVM> GetBySearch(string filter);
    }
}
