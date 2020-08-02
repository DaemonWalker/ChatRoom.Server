using ChatRoom.Server.Utils;
using CSRedis;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace ChatRoom.Server.DistributeSession
{
    public class DistributeSession : ISession
    {
        private readonly CSRedisClient redisDB;
        private readonly ILogger<DistributeSession> logger;
        private const string CACHE_SET_NAME = "NSESSIONID_";

        public string Id { get; internal set; }

        public bool IsAvailable => true;

        public IEnumerable<string> Keys => redisDB.HKeys(this.Id);

        public DistributeSession(IConfiguration config, ILogger<DistributeSession> logger)
        {
            this.logger = logger;

            var redisConfig = config.GetSection("Redis");
            var redisContr = $"{redisConfig["Address"]},defaultDatabase={redisConfig["DefaultDatabase"]},password={redisConfig["Password"]}";
            redisDB = new CSRedisClient(redisContr);
            RedisHelper.Initialization(redisDB);
        }
        public static string GenerateSessionId()
        {
            return $"{CACHE_SET_NAME}{Guid.NewGuid().ToString("N")}";
        }

        public void Clear()
        {
            redisDB.Del(this.Id);
        }

        public Task CommitAsync(CancellationToken cancellationToken = default)
        {
            throw new NotImplementedException();
        }

        public Task LoadAsync(CancellationToken cancellationToken = default)
        {
            throw new NotImplementedException();
        }

        public void Remove(string key)
        {
            redisDB.HDel(this.Id, key);
        }

        public void Set(string key, byte[] value)
        {
            if (key.IsValidSessionKey() == false)
            {
                throw new InvalidOperationException("SessionKey is invalid, cannot set value");
            }
            redisDB.HSet(this.Id, key, value);
        }

        public bool TryGetValue(string key, out byte[] value)
        {
            if (redisDB.HExists(this.Id, key))
            {
                value = redisDB.HGet<byte[]>(this.Id, key);
                return true;
            }
            else
            {
                value = null;
                return false;
            }
        }
    }
}
