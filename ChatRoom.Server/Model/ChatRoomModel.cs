using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ChatRoom.Server.Model
{
    public class ChatRoomModel
    {
        public string Name { get; set; }
        public string Id { get; set; }
        public string Password { get; set; }
        public string UserId { get; set; }
        public string Url { get; set; }
        public bool IsPublic { get; set; }
    }
}
