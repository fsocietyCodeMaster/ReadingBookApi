using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using ReadingBookApi.Model;
using ReadingBookApi.Repository;
using System.Security.Claims;

namespace ReadingBookApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BookController : ControllerBase
    {
        private readonly ILogger<TBookController> _logger;
        private readonly IBook _book;
        private readonly IReview _review;

        public BookController(ILogger<TBookController> logger, IBook book,IReview review)
        {

            _logger = logger;
            _book = book;
            _review = review;
        }


        [HttpGet("books")]
        public async Task<ActionResult<ResponseVM>> Books(int? page)
        {
            try
            {
                var pageNumber = page ?? 1;
                var pageSize = 4;

                var result =  await _book.GetAllByPage(pageNumber, pageSize);
                if (result.IsSuccess == true)
                { 
                    return Ok(result);
                }
                else
                {
                    return BadRequest(result);
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



        [HttpGet("book")]
        public async Task<ActionResult<ResponseVM>> SearchBooks(string search)
        {

            try
            {

                var result = await _book.GetBySearch(search);
                if (result.IsSuccess == true)
                {
                    return Ok(result);
                }
                else
                {
                    return BadRequest(result);
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


        [HttpPost("addreview")]
        public async Task<ActionResult> AddReview(Guid BookId,ReviewVM review)
        {
            if (ModelState.IsValid)
            {
                var user = User.FindFirstValue(ClaimTypes.NameIdentifier);
                try
                {
                    var bookResult = await _book.GetBookId(BookId);
                    review.BookId = bookResult.BookId;
                    review.UserId = user;
                    var reviewResult = await _review.AddReview(review);

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
        public async Task<ActionResult<ResponseVM>> UpdateReview(Guid id, JsonPatchDocument<ReviewVM> review)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    var result = await _review.UpdatePartial(id, review);
                    if (result.IsSuccess == true)
                    {
                        return Ok(result);
                    }
                    else
                    {
                        return BadRequest(result);
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




        [HttpPost("deletebook")]
        public async Task<ActionResult<ResponseVM>> DeleteBook(Guid id)
        {
            try
            {
                var result = await _review.Delete(id);
                if (result.IsSuccess == true)
                {
                    return Ok(result);
                }
                else
                {
                    return BadRequest(result);
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





    }
    
}
