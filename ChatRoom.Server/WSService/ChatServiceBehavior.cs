using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebSocketSharp;
using WebSocketSharp.Server;

namespace ChatRoom.Server.WSService
{
    public class ChatServiceBehavior : WebSocketBehavior
    {
        private readonly string roomId;
        public ChatServiceBehavior(string roomId)
        {
            this.roomId = roomId;
        }
        public ChatServiceBehavior() : this(string.Empty) { }

        protected override void OnMessage(MessageEventArgs e)
        {
            Sessions.Broadcast(e.Data);
        }
        protected override void OnOpen()
        {
            base.OnOpen();
        }
        protected override void OnClose(CloseEventArgs e)
        {
            base.OnClose(e);
        }
    }
}
