using Microsoft.AspNetCore.JsonPatch;
using ReadingBookApi.Model;

namespace ReadingBookApi.Repository
{
    public interface IBook
    {
        Task<ResponseVM> AddBook(BookVM book);
        Task<ResponseVM> GetAll();
        Task<ResponseVM> GetAllByPage(int page , int size);
        Task<ResponseVM> Get(Guid id);
        Task<T_Book> GetBookId(Guid id);
        Task<ResponseVM> Delete(Guid id);
        Task<ResponseVM> Update(Guid id ,BookVM book);
        Task<ResponseVM> UpdatePartial(Guid id , JsonPatchDocument<BookVM> book);
        Task<ResponseVM> GetBySearch(string filter);
    }
}
