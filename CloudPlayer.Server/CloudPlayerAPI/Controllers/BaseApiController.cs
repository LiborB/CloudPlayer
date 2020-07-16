using System;
using System.Linq;
using CloudPlayerAPI.Data;
using CloudPlayerAPI.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Internal;

namespace CloudPlayerAPI.Controllers
{
    public class BaseApiController : ControllerBase
    {
        protected CloudPlayerContext _context;

        protected User UserTokenHandler()
        {
            var token = GetUserToken();
            if (!String.IsNullOrEmpty(token))
            {
                var tokenExists = _context.User.Any(x => x.Token == token);
                if (!tokenExists)
                {
                    throw new Exception("You must be logged in to access this api");
                }

                return _context.User.First(x => x.Token == token);
            }
            else
            {
                throw new Exception("You must be logged in to access this api");
            }
        }

        private string GetUserToken()
        {
            var token = Request.Headers["user_token"];
            return token;
        }

        protected User GetCurrentUser()
        {
            var token = GetUserToken();
            return _context.User.First(x => x.Token == token);
        }
    }
}