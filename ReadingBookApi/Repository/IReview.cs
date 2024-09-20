﻿using Microsoft.AspNetCore.JsonPatch;
using ReadingBookApi.Model;

namespace ReadingBookApi.Repository
{
    public interface IReview
    {
        Task<ResponseVM> AddReview(ReviewVM review,string user,Guid bookId);
        Task<ResponseVM> UpdatePartial(Guid id, JsonPatchDocument<ReviewVM> book,string userId);
        Task<ResponseVM> Delete(Guid id,string userId);
        Task<ResponseVM> GetAll(string userId);
    }
}
