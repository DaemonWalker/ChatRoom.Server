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

        public static IApplicationBuilder UseDistributeSession(this IApplicationBuilder app)
        {
            if (app.CheckServicesAreRegisted() == false)
            {
                throw new InvalidOperationException("Please use IServiceCollection.AddDistributeSession first");
            }
            app.UseMiddleware<DistributeSessionMiddleware>();
            return app;
        }
        public static IServiceCollection AddDistributeSession(this IServiceCollection serviceDescriptors)
        {
            serviceDescriptors.AddTransient<ISession, DistributeSession>();
            return serviceDescriptors;
        }
    }
}
