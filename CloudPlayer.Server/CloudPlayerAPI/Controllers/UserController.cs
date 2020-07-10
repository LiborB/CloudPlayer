using System;
using System.Linq;
using System.Net;
using System.Net.Http;
using CloudPlayerAPI.Utility;
using CloudPlayerAPI.ViewModel;
using Microsoft.AspNetCore.Mvc;

namespace CloudPlayerAPI.Controllers
{
    [Route("api/users")]
    [ApiController]
    public class UserController : BaseApiController
    {
        [Route("login")]
        [HttpPost]
        public IActionResult LoginUserReturnToken(UserLoginVM userLoginVm)
        {
            var user = _context.User.FirstOrDefault(x => x.Username == userLoginVm.Username);
            if (user == null)
            {
                return Unauthorized();
            }

            var isPasswordCorrect =
                PasswordHasher.isPasswordCorrect(userLoginVm.Password, user.PasswordHash, user.PasswordSalt);
            if (isPasswordCorrect)
            {
                user.Token = Guid.NewGuid().ToString();
                _context.SaveChanges();
                return Ok(user.Token);
            }
            else
            {
                return Unauthorized();
            }
        }

        [Route("register")]
        [HttpPost]
        public IActionResult RegisterUser(UserRegisterVM userRegisterVm)
        {
            return null;
        }

        public bool IsUsernameAvailable(string username)
        {
            return !_context.User.Any(x => x.Username.ToUpper() == username.ToUpper());
        }
    }
}