using ChatRoom.Server.Model;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ChatRoom.Server.Utils
{
    public static class SessionExtenssion
    {
        const string KEY_USERREGISTED = "IsRegisted";
        const string KEY_USERID = "UserId";
        public static void SetUserIsTemp(this ISession session)
        {
            session.SetInt32(KEY_USERREGISTED, 0);
        }
        public static void SetUserIsRegisted(this ISession session)
        {
            session.SetInt32(KEY_USERREGISTED, 1);
        }
        public static void SetUserId(this ISession session, string userId)
        {
            session.SetString(KEY_USERID, userId);
        }
        public static string GetUserId(this ISession session)
        {
            return session.GetString(KEY_USERID);
        }
        public static bool GetUserIsRegisted(this ISession session)
        {
            return session.GetInt32(KEY_USERREGISTED) == 1;
        }
    }
}
