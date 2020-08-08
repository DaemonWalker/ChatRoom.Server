using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ChatRoom.Server.Model
{
    public class MessageModel
    {
        public string Content { get; set;}
        public string Date { get; set; }
        public bool IsNotify { get; set; }
        public ChatUserModel User { get; set; }
    }
}
