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
    }
}
