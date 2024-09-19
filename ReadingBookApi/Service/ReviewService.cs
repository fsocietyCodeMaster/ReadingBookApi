using AutoMapper;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ReadingBookApi.Context;
using ReadingBookApi.Model;
using ReadingBookApi.Repository;
using System.Net;

namespace ReadingBookApi.Service
{
    public class ReviewService : IReview
    {

        private readonly BookDbContext _context;
        private readonly IMapper _map;

        public ReviewService(BookDbContext context, IMapper map)
        {
            _context = context;
            _map = map;
        }

        public async Task<ResponseVM> AddReview(ReviewVM  review)
        {
            if (review == null)
            {
                var error = new ResponseVM
                {
                    Message = "Something is wrong.",
                    IsSuccess = false,
                    Status = HttpStatusCode.BadRequest.ToString()
                };
                return error;
            }
            else
            {
                var reviewMaped = _map.Map<T_Review>(review);
                _context.Add(reviewMaped);
                await _context.SaveChangesAsync();

                var success = new ResponseVM
                {
                    Message = "Review successfully added.",
                    IsSuccess = true,
                    Status = HttpStatusCode.OK.ToString(),
                };
                return success;
            }
        }

        public async Task<ResponseVM> Delete(Guid id)
        {
            var review = await _context.t_Review_Ratings.FirstOrDefaultAsync(c => c.ReviewId == id);
            if (review == null)
            {
                var error = new ResponseVM
                {
                    Message = "Something is wrong.",
                    IsSuccess = false,
                    Status = HttpStatusCode.BadRequest.ToString()
                };
                return error;
            }
            else
            {
                _context.Remove(review);

                var success = new ResponseVM
                {
                    Message = "Review successfully deleted.",
                    IsSuccess = true,
                    Status = HttpStatusCode.OK.ToString()
                };
                return success;
            }
        }

        public async Task<ResponseVM> GetAll()
        {
            var review = await _context.t_Review_Ratings.ToListAsync();

            if (review != null && review.Any())
            {
                var success = new ResponseVM
                {
                    Message = "Reviews successfully retrieved .",
                    IsSuccess = true,
                    Status = HttpStatusCode.OK.ToString(),
                    Data = new { response = _map.Map<IEnumerable<ReviewVM>>(review) }
                };
                return success;
            }
            else
            {
                var error = new ResponseVM
                {
                    Message = "Nothing found.",
                    IsSuccess = false,
                    Status = HttpStatusCode.NotFound.ToString(),
                    Data = new T_Review { }
                };
                return error;
            }
        }

      

        public async Task<ResponseVM> UpdatePartial(Guid id, JsonPatchDocument<ReviewVM> review)
        {
            if (review == null)
            {
                var error = new ResponseVM
                {
                    Message = "Nothing found.",
                    IsSuccess = false,
                    Status = HttpStatusCode.NotFound.ToString(),
                    Data = new ReviewVM { }
                };
                return error;

            }
            else
            {

                var reviewDb = await _context.t_Review_Ratings.FirstOrDefaultAsync(c => c.ReviewId == id);
                if (reviewDb != null)
                {

                    var reviewMapped = _map.Map<ReviewVM>(reviewDb);

                    review.ApplyTo(reviewMapped);

                    var bookPatched = _map.Map(reviewMapped, reviewDb);
                    await _context.SaveChangesAsync();


                    var success = new ResponseVM
                    {
                        Message = $"Review successfully updated .",
                        IsSuccess = true,
                        Status = HttpStatusCode.OK.ToString(),
                        Data = new { response = _map.Map<BookVM>(bookPatched) }
                    };
                    return success;

                }
                else
                {
                    var error = new ResponseVM
                    {
                        Message = "Nothing found.",
                        IsSuccess = false,
                        Status = HttpStatusCode.NotFound.ToString(),
                        Data = new ReviewVM { }
                    };
                    return error;
                }


            }
        }
    }
}
