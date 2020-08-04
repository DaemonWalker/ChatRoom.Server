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
        private readonly string redisConnStr;
        public ChatRoomService(IConfiguration config, int port = 7181)
        {
            server = new WebSocketServer(port);
            server.Start();

            var redisConfig = config.GetSection("Redis");
            redisConnStr = $"{redisConfig["Address"]},defaultDatabase={redisConfig["DefaultDatabase"]},password={redisConfig["Password"]}";
        }
        public bool OpenRoom(string roomId)
        {
            try
            {
#pragma warning disable CS0618 // 这个库好长时间不更新了 过时了就过时了吧 -_-||
                var behavior = new ChatServiceBehavior(roomId, this.redisConnStr);
                server.AddWebSocketService($"/{roomId}", () => new ChatServiceBehavior(roomId, redisConnStr));
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
