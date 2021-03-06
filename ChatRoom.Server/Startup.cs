using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ChatRoom.Server.Data;
using ChatRoom.Server.DistributeSession;
using ChatRoom.Server.Utils;
using ChatRoom.Server.WSService;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace ChatRoom.Server
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllers();
            services.AddCors(options => options.AddDefaultPolicy(builder => builder.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader()));
            services.AddDistributeSession(config =>
            {
                config.IsApiMode = true;
                config.RedisConnectionString = Configuration.GetRedisConnectionString();
            });
            services.AddChatRoom(config =>
            {
                config.Port = Convert.ToInt32(Configuration["WSPort"]);
                config.RedisConnectionString = Configuration.GetRedisConnectionString();
            });

            services.AddScoped<IDatabase, MySqlDatabase>();
            services.AddScoped<IStatistics, Statistics>();

            services.AddLogging(config => config.AddConsole());

        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, IServiceProvider service)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseCors();

            app.UseAuthorization();

            app.UseDistributeSession();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });

            app.UseChatRoom(service);
        }
    }
}
