using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http.Headers;
using System.Threading.Tasks;

namespace ChatRoom.Server.DistributeSession
{
    public class DistributeSessionMiddleware
    {
        const string SESSION_NAME = "NSessionId";
        private readonly RequestDelegate _next;
        private readonly DistributeSessionConfig config;
        public DistributeSessionMiddleware(RequestDelegate next, DistributeSessionConfig config)
        {
            this._next = next;
            this.config = config;
        }
        public async Task InvokeAsync(HttpContext context)
        {
            var sessionId = string.Empty;
            if (this.config.IsApiMode)
            {
                sessionId = GetSessionIdByHeader(context);
            }
            else
            {
                GetSessionIdByCookies(context);
            }

            var session = context.RequestServices.GetService<ISession>();
            session.SetSessionId(sessionId);
            context.Session = session;


            await _next(context);
        }
        private string GetSessionIdByCookies(HttpContext context)
        {
            var cookies = context.Request.Cookies;
            string sessionId = default;
            if (cookies.ContainsKey(SESSION_NAME))
            {
                sessionId = cookies[SESSION_NAME];
            }
            else
            {
                sessionId = DistributeSession.GenerateSessionId();
                context.Response.Cookies.Append(SESSION_NAME, sessionId);
            }
            return sessionId;
        }
        private string GetSessionIdByHeader(HttpContext context)
        {
            var authId = context.Request.Headers[HttpRequestHeader.Authorization.ToString()];
            return authId;
        }
    }
}
