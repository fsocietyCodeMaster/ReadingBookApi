using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using ReadingBookApi.Model;
using ReadingBookApi.Repository;
using System.Security.Claims;

namespace ReadingBookApi.Controllers
{
    [Authorize(Roles = "user")]
    [Route("api/[controller]")]
    [ApiController]
    public class ReviewController : ControllerBase
    {
        private readonly ILogger<TBookController> _logger;
        private readonly IBook _book;
        private readonly IReview _review;

        public ReviewController(ILogger<TBookController> logger, IBook book, IReview review)
        {

            _logger = logger;
            _book = book;
            _review = review;
        }


        [HttpPost("addreview")]
        public async Task<ActionResult> Add(Guid BookId, ReviewVM review)
        {
            if (ModelState.IsValid)
            {
                var user = User.FindFirstValue(ClaimTypes.NameIdentifier).ToString();
                try
                {
                    var bookResult = await _book.GetBookId(BookId);
                    var bookId = bookResult.BookId;
                    var reviewResult = await _review.AddReview(review, user, bookId);

                    if (reviewResult.IsSuccess == true)
                    {
                        return Ok(reviewResult);
                    }
                    else
                    {
                        return BadRequest(reviewResult);
                    }
                }
                catch (Exception ex)
                {

                    _logger.LogError(ex, "an error occurred.");

                    var error = new ResponseVM
                    {
                        Message = "Error.",
                        IsSuccess = false,
                        Status = StatusCodes.Status404NotFound.ToString()
                    };

                    return BadRequest(error);
                }
            }
            return BadRequest("Some parameters not valid.");
        }





        [HttpPatch("editreview")]
        public async Task<ActionResult<ResponseVM>> Update(Guid id, JsonPatchDocument<ReviewVM> review)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    if (User.Identity.IsAuthenticated)
                    {
                        var user = User.FindFirstValue(ClaimTypes.NameIdentifier).ToString();
                        var result = await _review.UpdatePartial(id, review, user);
                        if (result.IsSuccess == true)
                        {
                            return Ok(result);
                        }
                        else
                        {
                            return BadRequest(result);
                        }
                    }
                    else
                    {
                        return Unauthorized();
                    }
                }
                catch (Exception ex)
                {

                    _logger.LogError(ex, "an error occurred.");

                    var error = new ResponseVM
                    {
                        Message = "Error.",
                        IsSuccess = false,
                        Status = StatusCodes.Status404NotFound.ToString()
                    };

                    return BadRequest(error);
                }

            }
            return BadRequest("Some parameters not valid.");
        }




        [HttpDelete("deletereview")]
        public async Task<ActionResult<ResponseVM>> Delete(Guid id)
        {
            try
            {
                if (User.Identity.IsAuthenticated)
                {
                    var user = User.FindFirstValue(ClaimTypes.NameIdentifier).ToString();
                    var result = await _review.Delete(id, user);
                    if (result.IsSuccess == true)
                    {
                        return Ok(result);
                    }
                    else
                    {
                        return BadRequest(result);
                    }
                }
                else
                {
                    return Unauthorized();
                }
            }
            catch (Exception ex)
            {

                _logger.LogError(ex, "an error occurred.");

                var error = new ResponseVM
                {
                    Message = "Error.",
                    IsSuccess = false,
                    Status = StatusCodes.Status404NotFound.ToString()
                };

                return BadRequest(error);
            }

        }

        [HttpGet]
        public async Task<ActionResult<ResponseVM>> GetAllReview()
        {
            if (User.Identity.IsAuthenticated)
            {
                var user = User.FindFirstValue(ClaimTypes.NameIdentifier).ToString();
                return await _review.GetAll(user);
            }
            else
            {
                return Unauthorized();
            }

        }

    }
}
