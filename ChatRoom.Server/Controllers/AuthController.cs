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
        public string SignIn([FromBody]UserModel user)
        {
            user = database.UserSignIn(user);
            if (user.IsDefault())
            {
                return string.Empty;
            }

            this.HttpContext.Session.SetUserIsRegisted();
            this.HttpContext.Session.SetUserId(user.UserId);
            return this.HttpContext.Session.Id;
        }

        [HttpPost]
        public void SignOut()
        {
            this.HttpContext.Session.Clear();
        }
    }
}
