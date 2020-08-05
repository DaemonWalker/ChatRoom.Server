using CSRedis;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebSocketSharp.Server;

namespace ChatRoom.Server.WSService
{
    public class ChatRoomService
    {
        private readonly WebSocketServer server;
        private readonly ChatRoomConfig config;
        public ChatRoomService(ChatRoomConfig config)
        {
            this.config = config;
            server = new WebSocketServer(config.Port);
            server.Start();

        }
        public bool OpenRoom(string roomId)
        {
            try
            {
#pragma warning disable CS0618 // 这个库好长时间不更新了 过时了就过时了吧 -_-||
                server.AddWebSocketService($"/{roomId}", () => new ChatServiceBehavior(roomId, config.RedisConnectionString));
#pragma warning restore CS0618 // 这个库好长时间不更新了 过时了就过时了吧 -_-||
                return true;
            }
            catch
            {
                return false;
            }
        }
        public void OpenDefault()
        {
            OpenRoom("hall");
        }
    }
}
