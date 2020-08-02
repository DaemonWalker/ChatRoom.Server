using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ChatRoom.Server.DistributeSession
{
    public static class DistibuteSessionExtenssion
    {
        internal static void SetSessionId(this ISession session, string sessionId)
        {
            if (session is DistributeSession && session != null)
            {
                (session as DistributeSession).Id = sessionId;
            }
            else if (session is not DistributeSession)
            {
                throw new InvalidOperationException($"This extenssion is only use for {typeof(DistributeSession).FullName}");
            }
            else
            {
                throw new InvalidOperationException("Session is null, can not set");
            }
        }
        internal static bool CheckServicesAreRegisted(this IApplicationBuilder app)
        {
            return app.ApplicationServices.GetService<ISession>() is DistributeSession;
        }
        internal static bool IsValidSessionKey(this string key)
        {
            if (string.IsNullOrEmpty(key))
            {
                return false;
            }
            else
            {
                return true;
            }
        }

        public static IApplicationBuilder UseDistributeSession(this IApplicationBuilder app)
        {
            return UseDistributeSession(app, null);
        }
        public static IApplicationBuilder UseDistributeSession(this IApplicationBuilder app,Action<DistributeSessionConfig> configAction)
        {
            if (app.CheckServicesAreRegisted() == false)
            {
                throw new InvalidOperationException("Please use IServiceCollection.AddDistributeSession first");
            }
            var config = new DistributeSessionConfig();
            configAction?.Invoke(config);
            app.UseMiddleware<DistributeSessionMiddleware>(config);
            return app;
        }
        public static IServiceCollection AddDistributeSession(this IServiceCollection serviceDescriptors)
        {
            serviceDescriptors.AddTransient<ISession, DistributeSession>();
            return serviceDescriptors;
        }
    }
}
