using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.OpenApi.Models;
using Simulator.Engine.Directory;
using Simulator.Engine.Hubs;
using Simulator.Engine.Services;

namespace Simulator.Engine
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
            services.AddRouting(options =>
            {
                options.LowercaseUrls = true;
            });
            
            services.AddSignalR();
            services.AddSingleton<IMessageProcessor, MessageProcessor>();
            
            services.AddCors(options =>
            {
                options.AddPolicy("ClientPermission", policy =>
                {
                    policy.AllowAnyHeader()
                        .AllowAnyMethod()
                        .WithOrigins("http://localhost:3000")
                        .AllowCredentials();
                });
            });

            services.AddSingleton<IStore, Store>();
            
            services.AddSingleton<IProcessor, AkkaService>();
            services.AddHostedService<AkkaService>(sp => (AkkaService) sp.GetRequiredService<IProcessor>());

            services.AddTransient<Seeder>();
            
            services.AddControllers();
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo {Title = "Simulator.Engine", Version = "v1"});
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, Seeder seeder)
        {
            seeder.SeedAsync().Wait();
            
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "Simulator.Engine v1"));
            }

            app.UseHttpsRedirection();

            app.UseRouting();
            
            app.UseCors("ClientPermission");

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
                endpoints.MapHub<MessageHub>("/hubs/message");
            });
        }
    }
}