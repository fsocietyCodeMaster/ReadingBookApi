using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using ReadingBookApi.Model;
using ReadingBookApi.Repository;

namespace ReadingBookApi.Controllers
{
    [Authorize(Roles ="admin")]
    [Route("api/[controller]")]
    [ApiController]
    public class BookAdminController : ControllerBase
    {
        private readonly ILogger<BookAdminController> _logger;
        private readonly IBook _book;
        public BookAdminController(ILogger<BookAdminController> logger, IBook book)
        {

            _logger = logger;
            _book = book;
        }

        
        [HttpGet("All")]
        public async Task<ActionResult<ResponseVM>> GetAll(int? page)
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



        [HttpGet("Details")]
        public async Task<ActionResult<ResponseVM>> Get(Guid id,int? page)
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


        [HttpPost("Add")]
        public async Task<ActionResult<ResponseVM>> Add(BookDetailVM book)
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


        [HttpPost("Delete")]
        public async Task<ActionResult<ResponseVM>> Delete(Guid id)
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





        [HttpPut("Edit")]
        public async Task<ActionResult<ResponseVM>> Update(Guid id, BookDetailVM book)
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





        [HttpPatch("Edit-Partial")]
        public async Task<ActionResult<ResponseVM>> UpdatePartial(Guid id, JsonPatchDocument<BookDetailVM> book)
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

