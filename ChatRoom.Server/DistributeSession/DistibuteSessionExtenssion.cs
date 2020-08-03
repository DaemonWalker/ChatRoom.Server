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
            if (app.CheckServicesAreRegisted() == false)
            {
                throw new InvalidOperationException("Please use IServiceCollection.AddDistributeSession first");
            }
            
            app.UseMiddleware<DistributeSessionMiddleware>();
            return app;
        }
        public static IServiceCollection AddDistributeSession(this IServiceCollection services,Action<DistributeSessionConfig> configAction)
        {
            var config = new DistributeSessionConfig();
            configAction?.Invoke(config);
            services.AddHttpContextAccessor();
            services.AddTransient<ISession, DistributeSession>();
            return services;
        }
        public static IServiceCollection AddDistributeSession(this IServiceCollection services)
        {
            return AddDistributeSession(services, null);
        }
    }
}
