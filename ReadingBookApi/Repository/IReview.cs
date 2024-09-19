using Microsoft.AspNetCore.JsonPatch;
using ReadingBookApi.Model;

namespace ReadingBookApi.Repository
{
    public interface IReview
    {
        Task<ResponseVM> AddReview(ReviewVM review);
        Task<ResponseVM> UpdatePartial(Guid id, JsonPatchDocument<ReviewVM> book);
        Task<ResponseVM> Delete(Guid id);
        Task<ResponseVM> GetAll();
    }
}
