using ReadingBookApi.Model;
using ReadingBookApi.Repository;
using Microsoft.AspNetCore.Mvc;

namespace BazresiApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IUser _user;
        private readonly ILogger<AuthController> _logger;
        public AuthController(IUser user, ILogger<AuthController> logger)
        {
            _user = user;
            _logger = logger;
        }

        [HttpPost("Register")]
        public async Task<IActionResult> Register(RegisterVM register)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var result = await _user.RegisterAsync(register);
                    if (result.isSuccess)
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
                    return BadRequest("some properties are not valid ");
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "an error occurred.");

                var error = new ResponseVM
                {
                    Message = "Error.",
                    IsSuccess = false,
                };

                return BadRequest(error);
            }
        }


        [HttpPost("Login")]
        public async Task<IActionResult> Login(LoginVM login)
        {
            if (ModelState.IsValid)
            {
                try
                {

                    var result = await _user.LoginAsync(login);

                    if (result.isSuccess)
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
                    };

                    return BadRequest(error);
                }


            }
            else
            {
                return BadRequest("some properties are not valid ");
            }

        }

    }
}
