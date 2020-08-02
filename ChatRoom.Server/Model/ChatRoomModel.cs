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
        public string OwnerUserId { get; set; }
        public string Link { get; set; }
    }
}
