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
        public ChatRoomService(int port = 7181)
        {
            server = new WebSocketServer(port);
            server.Start();
        }
        public bool OpenRoom(string roomId)
        {
            try
            {
#pragma warning disable CS0618 // 这个库好长时间不更新了 过时了就过时了吧 -_-||
                server.AddWebSocketService($"/{roomId}", () => new ChatServiceBehavior(roomId));
#pragma warning restore CS0618 // 这个库好长时间不更新了 过时了就过时了吧 -_-||
                return true;
            }
            catch
            {
                return false;
            }
        }
    }
}
