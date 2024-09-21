using Microsoft.AspNetCore.Mvc;
using ReadingBookApi.Model;
using ReadingBookApi.Repository;


namespace ReadingBookApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BookController : ControllerBase
    {
        private readonly ILogger<BookAdminController> _logger;
        private readonly IBook _book;

        public BookController(ILogger<BookAdminController> logger, IBook book)
        {

            _logger = logger;
            _book = book;
        }


        [HttpGet("All")]
        public async Task<ActionResult<ResponseVM>> Books(int? page)
        {
            try
            {
                var pageNumber = page ?? 1;
                var pageSize = 2;

                var result = await _book.GetAllByPage(pageNumber, pageSize);
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
        public async Task<ActionResult<ResponseVM>> Book(Guid id, int? page)
        {
            try
            {
                var pageReviewNumber = page ?? 1;
                var pageReviewSize = 2;

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


        [HttpGet("Search")]
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







    }

}
