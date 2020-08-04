using ChatRoom.Server.Model;
using CSRedis;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;
using WebSocketSharp;
using WebSocketSharp.Server;

namespace ChatRoom.Server.WSService
{
    public class ChatServiceBehavior : WebSocketBehavior
    {
        private readonly string roomId;
        private readonly JsonSerializerOptions jsonSerializerOptions;
        private readonly CSRedisClient redisDB;
        private const string RANKNAME_SPEACH = "Speach";
        //protected WebSocketSessionManager aliveSession =>this.Sessions.Sessions.Where(p=>p.Context.a)

        public ChatServiceBehavior(string roomId, string redisConnStr)
        {
            this.roomId = roomId;
            jsonSerializerOptions = new JsonSerializerOptions()
            {
                PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
                IgnoreNullValues = true
            };
            this.redisDB = new CSRedisClient(redisConnStr);
            this.redisDB.SubscribeListBroadcast(this.GetChannelId(), new Random().Next().ToString(), msg => this.Broadcast(msg));
        }

        protected override void OnMessage(MessageEventArgs e)
        {
            var model = Deserialize(e.Data);
            model.Date = DateTime.Now.ToString("yyyy/MM/dd HH:mm:ss");

            if (model.User.IsTempUser)
            {
                this.redisDB.ZAddAsync(RANKNAME_SPEACH, (1, model.User.UserId));
            }

            var json = Serialize(model);
            this.redisDB.PublishAsync(this.GetChannelId(), json);

            Sessions.Broadcast(json);
        }
        protected override void OnOpen()
        {
            base.OnOpen();
        }
        protected override void OnClose(CloseEventArgs e)
        {
            base.OnClose(e);
        }
        protected override void OnError(ErrorEventArgs e)
        {
            base.OnError(e);
        }
        public void Broadcast(string json)
        {
            if (string.IsNullOrEmpty(json) == false)
            {
                Sessions?.Broadcast(json);
            }
        }

        protected virtual string GetChannelId()
        {
            return this.roomId;
        }

        protected virtual MessageModel Deserialize(string json)
        {
            return JsonSerializer.Deserialize<MessageModel>(json, jsonSerializerOptions);
        }
        protected virtual string Serialize(MessageModel message)
        {
            return JsonSerializer.Serialize<MessageModel>(message, jsonSerializerOptions);
        }
    }
}
