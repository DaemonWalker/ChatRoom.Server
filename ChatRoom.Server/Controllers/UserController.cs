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
    [Route("/api/[controller]")]
    public class UserController : ControllerBase
    {
        private readonly IDatabase database;
        public UserController(IDatabase database)
        {
            this.database = database;
        }

        [HttpPost]
        public string CreateTemp()
        {
            var tempUserId = Guid.NewGuid().ToString("N");
            this.HttpContext.Session
                .SetUserIsTemp()
                .SetUserId(tempUserId);

            return this.HttpContext.Session.Id;
        }

        [HttpPost]
        public string SignUp([FromBody] UserModel user)
        {
            try
            {
                database.CreateUser(user);
                return "OK";
            }
            catch
            {
                return "Error";
            }
        }
    }
}
