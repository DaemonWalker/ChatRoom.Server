using ChatRoom.Server.DistributeSession;
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
    public class TestController : ControllerBase
    {
        [HttpGet]
        [Route("{value}")]
        public string Set(string value)
        {
            this.HttpContext.Session.SetString("test", value);
            return "OK";
        }

        [HttpGet]
        public string Get()
        {
            return this.HttpContext.Session.GetString("test");
        }
    }
}
