using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ChatRoom.Server.Controllers
{
    public abstract class ChatControllerBase:ControllerBase
    {
        protected object Session { get; }
    }
}
