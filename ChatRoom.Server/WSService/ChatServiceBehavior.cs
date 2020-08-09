using ChatRoom.Server.Model;
using CSRedis;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;
using WebSocketSharp.NetCore;
using WebSocketSharp.NetCore.Server;
using static CSRedis.CSRedisClient;

namespace ChatRoom.Server.WSService
{
    public class ChatServiceBehavior : WebSocketBehavior
    {
        public string RoomId { get; internal set; }
        private readonly JsonSerializerOptions jsonSerializerOptions;
        private readonly ChatRoomRedisService subscribeService;
        private readonly ILogger<ChatServiceBehavior> logger;

        public ChatServiceBehavior(ILogger<ChatServiceBehavior> logger, ChatRoomRedisService subscribeService)
        {
            this.logger = logger;
            this.subscribeService = subscribeService;
            jsonSerializerOptions = new JsonSerializerOptions()
            {
                PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
                IgnoreNullValues = true
            };
        }

        protected override void OnMessage(MessageEventArgs e)
        {
            var model = Deserialize(e.Data);
            model.Date = DateTime.Now.ToString("yyyy/MM/dd HH:mm:ss");

            if (model.User.IsTempUser)
            {
                this.subscribeService.AddSpeach(model.User.UserId);
            }

            var json = Serialize(model);
            this.subscribeService.SendMessage(this.GetChannelId(), json);

            //Sessions.Broadcast(json);
        }
        protected override void OnOpen()
        {
            base.OnOpen();
        }
        protected override void OnClose(CloseEventArgs e)
        {
            base.OnClose(e);
            this.subscribeService.RemoveBehavior(this);
        }
        protected override void OnError(ErrorEventArgs e)
        {
            base.OnError(e);
            this.subscribeService.RemoveBehavior(this);
        }
        public void Broadcast(string json)
        {
            if (string.IsNullOrEmpty(json) == false)
            {
                logger.LogInformation(json);
                Sessions?.Broadcast(json);
            }
        }

        protected virtual string GetChannelId()
        {
            return this.RoomId;
        }

        protected virtual MessageModel Deserialize(string json)
        {
            return JsonSerializer.Deserialize<MessageModel>(json, jsonSerializerOptions);
        }
        protected virtual string Serialize(MessageModel message)
        {
            return JsonSerializer.Serialize(message, jsonSerializerOptions);
        }
    }
}
