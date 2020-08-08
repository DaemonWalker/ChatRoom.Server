using ChatRoom.Server.Model;
using ChatRoom.Server.Utils;
using CSRedis;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ChatRoom.Server.Data
{
    public class Statistics : IStatistics
    {
        private readonly CSRedisClient redisClient;
        private readonly IDatabase database;

        private const string RANKNAME_SPEACH = "Speach";
        public Statistics(IConfiguration config, IDatabase database)
        {
            this.database = database;
            redisClient = new CSRedisClient(config.GetRedisConnectionString());
            RedisHelper.Initialization(redisClient);
        }
        public List<SpeachStatisticsModel> SpeachRank(int count)
        {
            var rank = redisClient.ZRangeWithScores(Statistics.RANKNAME_SPEACH, 0, count);
            var userIds = rank.Select(p => p.member).ToList();
            var users = this.database.GetUsers(userIds);
            return rank
                .Select((data, index) =>
                new SpeachStatisticsModel()
                {
                    Count = Convert.ToInt32(data.score),
                    UserName = users.FirstOrDefault(p => p.Id == data.member).Name,
                    Rank = index + 1
                })
                .ToList();
        }
    }
}
