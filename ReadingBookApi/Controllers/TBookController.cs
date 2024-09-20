using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using ReadingBookApi.Model;
using ReadingBookApi.Repository;

namespace ReadingBookApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TBookController : ControllerBase
    {
        private readonly ILogger<TBookController> _logger;
        private readonly IBook _book;
        public TBookController(ILogger<TBookController> logger, IBook book)
        {

            _logger = logger;
            _book = book;
        }

        //[Authorize(Roles = "admin")]
        [HttpGet("books")]
        public async Task<ActionResult<ResponseVM>> GetAllBooks(int? page)
        {
            try
            {
                var pageNumber = page ?? 1;
                var pageSize = 4;

                var result = await _book.GetAllByPage(pageNumber,pageSize);
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
        public async Task<ActionResult<ResponseVM>> GetBook(Guid id,int? page)
        {
            try
            {
                var pageReviewNumber = page ?? 1;
                var pageReviewSize = 4;
                var result = await _book.Get(id, pageReviewNumber, pageReviewSize);
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


        [HttpPost("add")]
        public async Task<ActionResult<ResponseVM>> CreateBook(BookDetailVM book)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    var result = await _book.AddBook(book);
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
                var result = await _book.Delete(id);
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





        [HttpPut("editbook")]
        public async Task<ActionResult<ResponseVM>> UpdateBook(Guid id, BookDetailVM book)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    var result = await _book.Update(id, book);
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





        [HttpPatch("editpartialbook")]
        public async Task<ActionResult<ResponseVM>> UpdateBook(Guid id, JsonPatchDocument<BookDetailVM> book)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    var result = await _book.UpdatePartial(id,book);
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


    }
}

