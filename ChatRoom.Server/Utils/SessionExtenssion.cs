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
        const string KEY_TEMPUSER = "IsTempory";
        const string KEY_USERID = "UserId";
        public static void SetUserIsTemp(this ISession session)
        {
            session.SetInt32(KEY_TEMPUSER, 1);
        }
        public static void SetUserId(this ISession session,UserModel user)
        {
            session.SetString(KEY_USERID, user.Id);
        }
    }
}
