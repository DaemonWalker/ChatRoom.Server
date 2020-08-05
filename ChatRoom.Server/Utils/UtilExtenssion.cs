using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ChatRoom.Server.Utils
{
    public static class UtilExtenssion
    {
        public static string ToBase64String(this byte[] bytes)
        {
            return Convert.ToBase64String(bytes);
        }
        public static bool IsDefault<T>(this T t)
        {
            return object.Equals(t, default(T));
        }
        public static string GetRedisConnectionString(this IConfiguration config)
        {
            var redisConfig = config.GetSection("Redis");
            return $"{redisConfig["Address"]},defaultDatabase={redisConfig["DefaultDatabase"]},password={redisConfig["Password"]}";
        }
    }
}
