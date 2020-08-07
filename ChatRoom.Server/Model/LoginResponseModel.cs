using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ChatRoom.Server.Model
{
    public class LoginResponseModel
    {
        public string UserId { get; set; }
        public string UserName { get; set; }
        public string SessionId { get; set; }
    }
}
