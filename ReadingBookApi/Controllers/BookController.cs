using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using ReadingBookApi.Model;
using ReadingBookApi.Repository;


namespace ReadingBookApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BookController : ControllerBase
    {
        private readonly ILogger<TBookController> _logger;
        private readonly IBook _book;

        public BookController(ILogger<TBookController> logger, IBook book)
        {

            _logger = logger;
            _book = book;
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


        




    }
    
}
