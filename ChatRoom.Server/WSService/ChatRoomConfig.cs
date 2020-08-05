using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ChatRoom.Server.WSService
{
    public class ChatRoomConfig
    {
        public string RedisConnectionString { get; set; }
        public int Port { get; set; }
    }
}
