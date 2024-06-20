using DogBarberShopBl.intarfaces;
using DogBarberShopDl.EF.Models;
using DogBarberShopEntitis;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace DogBarberShopApi.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IuserBl _userBl
          ;
        public UserController(IuserBl userBl)
        {
            this._userBl = userBl;
        }

        [HttpPost]
        public IActionResult AddUserName(AddUserDTo userDTO)
        {
            try
            {
                _userBl.AddUserName(userDTO);
                return Ok();
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }
        [HttpGet]
        public IActionResult GetAllUsers()
        {
            try
 {

                List<User> users = _userBl.GetAllUsers();
                return Ok(users);
            }
 catch (Exception ex)
 {

                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }
    }
}
