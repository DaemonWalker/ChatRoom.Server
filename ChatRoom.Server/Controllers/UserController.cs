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
            this.HttpContext.Session.SetUserIsTemp();
            return this.HttpContext.Session.Id;
        }

        [HttpPost]
        public void SignUp([FromBody]UserModel user)
        {
            database.CreateUser(user);
        }
    }
}
