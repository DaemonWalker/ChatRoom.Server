using CSRedis;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebSocketSharp.NetCore.Server;
using static CSRedis.CSRedisClient;

namespace ChatRoom.Server.WSService
{
    public class ChatRoomService
    {
        private readonly WebSocketServer server;
        private ChatRoomRedisService subscribeService;
        private readonly Dictionary<string, List<ChatServiceBehavior>> subscribeChannels = new Dictionary<string, List<ChatServiceBehavior>>();
        public ChatRoomService(ChatRoomRedisService subscribeService, ChatRoomConfig config)
        {
            this.subscribeService = subscribeService;
            server = new WebSocketServer(config.Port);
            server.Start();
        }
        public bool OpenRoom(string roomId)
        {
            try
            {
#pragma warning disable CS0618 // 这个库好长时间不更新了 过时了就过时了吧 -_-||
                server.AddWebSocketService($"/{roomId}", () => this.subscribeService.CrateBehavior(roomId));
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
