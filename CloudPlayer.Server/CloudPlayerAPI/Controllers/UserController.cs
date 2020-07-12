using System;
using System.Linq;
using System.Net;
using System.Net.Http;
using CloudPlayerAPI.Data;
using CloudPlayerAPI.Models;
using CloudPlayerAPI.Utility;
using CloudPlayerAPI.ViewModel;
using Microsoft.AspNetCore.Mvc;

namespace CloudPlayerAPI.Controllers
{
    [Route("api/users")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly CloudPlayerContext _context;

        public UserController(CloudPlayerContext context)
        {
            _context = context;
        }
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
            if (!IsUsernameAvailable(userRegisterVm.Username))
            {
                return Unauthorized();
            }

            var userToken = Guid.NewGuid().ToString();
            var hashedPasswordAndSalt = PasswordHasher.GeneratePasswordHashAndSalt(userRegisterVm.Password);
            _context.User.Add(new User()
            {
                Username = userRegisterVm.Username,
                PasswordHash = hashedPasswordAndSalt.Hash,
                PasswordSalt = hashedPasswordAndSalt.Salt,
                Created = DateTime.UtcNow,
                Token = userToken,
                RememberMe = false
            });
            _context.SaveChanges();
            return Ok(userToken);
        }

        [Route("isusernameavailable")]
        [HttpGet]
        public bool IsUsernameAvailable(string username)
        {
            if (string.IsNullOrEmpty(username))
            {
                return false;
            }
            return !UsernameTaken(username);
        }

        private bool UsernameTaken(string username)
        {
            return _context.User.Any(x => x.Username.ToUpper() == username.ToUpper());
        }

        [Route("istokenvalid")]
        [HttpGet]
        public bool IsTokenValid(string token)
        {
            return _context.User.Any(x => x.Token == token);
        }
    }
}