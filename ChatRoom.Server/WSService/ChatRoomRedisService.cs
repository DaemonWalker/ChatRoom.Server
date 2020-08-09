using CSRedis;
using Microsoft.AspNetCore.Mvc.Formatters;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebSocketSharp.NetCore;
using static CSRedis.CSRedisClient;

namespace ChatRoom.Server.WSService
{
    public class ChatRoomRedisService
    {
        private readonly ChatRoomConfig config;
        private readonly CSRedisClient redisDB;
        private IServiceProvider server;
        private ConcurrentDictionary<string, List<ChatServiceBehavior>> behaviors;
        private ConcurrentDictionary<string, SubscribeObject> subscribes;

        private const string RANKNAME_SPEACH = "Speach";

        public ChatRoomRedisService(IServiceProvider server, ChatRoomConfig config)
        {
            this.server = server;
            this.config = config;
            redisDB = new CSRedisClient(config.RedisConnectionString);
            behaviors = new ConcurrentDictionary<string, List<ChatServiceBehavior>>();
            subscribes = new ConcurrentDictionary<string, SubscribeObject>();
        }
        public ChatServiceBehavior CrateBehavior(string channelId)
        {
            if (subscribes.ContainsKey(channelId) == false)
            {
                var subscibe = redisDB.Subscribe((
                     channelId,
                     new Action<SubscribeMessageEventArgs>(args =>
                     {
                         behaviors[channelId].FirstOrDefault(p => p.ConnectionState == WebSocketState.Open).Broadcast(args.Body);
                     })));
                subscribes.TryAdd(channelId, subscibe);
            }

            if (behaviors.ContainsKey(channelId) == false)
            {
                behaviors.TryAdd(channelId, new List<ChatServiceBehavior>());
            }

            var behavior = server.GetService<ChatServiceBehavior>();
            behavior.RoomId = channelId;
            behaviors[channelId].Add(behavior);


            return behavior;
        }

        public void SendMessage(string channelId, string message)
        {
            this.redisDB.PublishAsync(channelId, message);
        }

        public void AddSpeach(string userId)
        {
            this.redisDB.ZIncrByAsync(RANKNAME_SPEACH, userId, 1);
        }

        public void RemoveBehavior(ChatServiceBehavior behavior)
        {
            if (this.behaviors[behavior.RoomId].Contains(behavior) == false)
            {
                return;
            }
            this.behaviors[behavior.RoomId].Remove(behavior);
            if (this.behaviors[behavior.RoomId].Count == 0)
            {
                var subscribe = this.subscribes[behavior.RoomId];
                if (subscribe.IsUnsubscribed == false)
                {
                    subscribe.Unsubscribe();
                }
                this.subscribes.TryRemove(new KeyValuePair<string, SubscribeObject>(behavior.RoomId, subscribe));
                subscribe.Dispose();
            }
        }
    }
}
