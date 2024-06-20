using DogBarberShopBl.intarfaces;
using DogBarberShopEntitis;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace DogBarberShopApi.Controllers
{
    [Authorize]
    [ApiController]
    [Route("api/[controller]/[action]")]
    public class UsersController : ControllerBase
    {
        private readonly ILogger<UsersController> _logger;
        private readonly IusersBl _usersBL;

        public UsersController(ILogger<UsersController> logger, IusersBl usersBL)
        {
            _logger = logger;
            _usersBL = usersBL;
        }


        [AllowAnonymous]
        [HttpPost]
        public IActionResult Login(UserLoginDTO user)
        {
            try
            {
                bool isExist = _usersBL.Login(user);
                if (!isExist)
                    return NotFound();
                return Ok(new
                {
                    userName =user.UserName,password = user.Password,

                    // ניתן להוסיף כאן עוד מאפיינים או נתונים על המשתמש שברצונך להחזיר
                });

            }
            catch (Exception ex)
            {
                _logger.LogError($"Error on Login, Message: {ex.Message}," +
                    $" InnerException: {ex.InnerException}, StackTrace: {ex.StackTrace}");
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }

        [HttpGet]
        public IActionResult Logout()
        {
            try
            {
                Response.Cookies.Delete(CookiesKeys.AccessToken);
                return Ok();
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error on Logout, Message: {ex.Message}," +
                                       $" InnerException: {ex.InnerException}, StackTrace: {ex.StackTrace}");
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }

    }
}
