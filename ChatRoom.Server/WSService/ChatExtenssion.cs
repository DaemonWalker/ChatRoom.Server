using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ChatRoom.Server.WSService
{
    public static class ChatExtenssion
    {
        public static IApplicationBuilder UseChatRoom(this IApplicationBuilder app, IServiceProvider service)
        {
            var chatRoomService = service.GetService<ChatRoomService>();
            chatRoomService.OpenDefault();
            return app;
        }

        public static IServiceCollection AddChatRoom(this IServiceCollection services, Action<ChatRoomConfig> configAction)
        {
            var config = new ChatRoomConfig();
            configAction.Invoke(config);
            services.AddSingleton(config);
            services.AddSingleton<ChatRoomService>();
            return services;
        }
    }
}
