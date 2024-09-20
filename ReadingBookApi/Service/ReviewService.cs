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

        public async Task<ResponseVM> AddReview(ReviewVM  review, string user, Guid bookId)
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
                var minRate = 1;
                var maxRate = 5;
                if (review.Rating < minRate || review.Rating > maxRate)
                {
                    var error = new ResponseVM
                    {
                        Message = "You should choose between 1 to 5.",
                        IsSuccess = false,
                        Status = HttpStatusCode.BadRequest.ToString()
                    };
                    return error;
                }
                else
                {
                    var reviewMaped = new T_Review()
                    {
                        UserId = user,
                        BookId = bookId,
                        Created = DateTimeOffset.Now.LocalDateTime,
                        Rating = review.Rating,
                        Comment = review.Comment,
                    };
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
        }

        public async Task<ResponseVM> Delete(Guid id, string userId)
        {
            var review = await _context.t_Review.FirstOrDefaultAsync(c => c.ReviewId == id);
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
                if (review.UserId == userId)
                {
                    _context.Remove(review);
                   await _context.SaveChangesAsync();
                    var success = new ResponseVM
                    {
                        Message = "Review successfully deleted.",
                        IsSuccess = true,
                        Status = HttpStatusCode.OK.ToString()
                    };
                    return success;
                }
                else
                {
                    var error = new ResponseVM
                    {
                        Message = "User not found.",
                        IsSuccess = false,
                        Status = HttpStatusCode.BadRequest.ToString()
                    };
                    return error;

                }
            }
        }

        public async Task<ResponseVM> GetAll(string userId)
        {
            var review = await _context.t_Review.ToListAsync();
            var user = string.Empty;
            foreach (var item in review)
            {
                 user = item.UserId;
            }
            if (review != null && review.Any() && userId == user)
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
                    Data = null
                };
                return error;
            }
        }

      

        public async Task<ResponseVM> UpdatePartial(Guid id, JsonPatchDocument<ReviewVM> review, string userId)
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

                var reviewDb = await _context.t_Review.FirstOrDefaultAsync(c => c.ReviewId == id);
                if (reviewDb != null && reviewDb.UserId == userId)
                {

                    var reviewMapped = _map.Map<ReviewVM>(reviewDb);

                    review.ApplyTo(reviewMapped);

                    var reviewPatched = _map.Map(reviewMapped, reviewDb);
                    await _context.SaveChangesAsync();


                    var success = new ResponseVM
                    {
                        Message = $"Review successfully updated .",
                        IsSuccess = true,
                        Status = HttpStatusCode.OK.ToString(),
                        Data = new { response = _map.Map<ReviewVM>(reviewPatched) }
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
                        Data = null
                    };
                    return error;
                }


            }
        }
    }
}
