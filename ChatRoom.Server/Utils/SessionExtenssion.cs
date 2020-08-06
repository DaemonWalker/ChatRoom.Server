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
        public static ISession SetUserIsTemp(this ISession session)
        {
            session.SetInt32(KEY_USERREGISTED, 0);
            return session;
        }
        public static ISession SetUserIsRegisted(this ISession session)
        {
            session.SetInt32(KEY_USERREGISTED, 1);
            return session;
        }
        public static ISession SetUserId(this ISession session, string userId)
        {
            session.SetString(KEY_USERID, userId);
            return session;
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
