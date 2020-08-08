using ChatRoom.Server.Data;
using ChatRoom.Server.Model;
using ChatRoom.Server.Utils;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ChatRoom.Server.Controllers
{
    [ApiController]
    [Route("/api/[controller]/[action]")]
    public class AuthController : ControllerBase
    {
        private readonly IDatabase database;
        public AuthController(IDatabase database)
        {
            this.database = database;
        }

        [HttpPost]
        public LoginResponseModel SignIn([FromBody] UserModel user)
        {
            var result = new LoginResponseModel();
            user = database.UserSignIn(user);
            if (user.IsDefault())
            {
                return result;
            }

            this.HttpContext.Session
                .SetUserIsRegisted()
                .SetUserId(user.Id);

            result.UserId = user.Id;
            result.UserName = user.Name;
            result.SessionId = this.HttpContext.Session.Id;

            return result;
        }

        [HttpPost]
        public bool SignOut()
        {
            try
            {
                this.HttpContext.Session.Clear();
                return true;
            }
            catch
            {
                return false;
            }
        }
    }
}
