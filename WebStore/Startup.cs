using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Configuration;
using WebStore.Infrastructure.Middleware;

namespace WebStore
{
    public class Startup
    {
        /// <summary>
        /// Добавляем свойство для доступа к конфигурации
        /// </summary>
        private IConfiguration Configuration { get; }

        /// <summary>
        /// Добавляем новый конструктор, принимающий интерфейс IConfiguration
        /// </summary>
        /// <param name="configuration"></param>
        public Startup(IConfiguration configuration)
        {
            this.Configuration = configuration;
        }

        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
            //Добавляем сервисы, необходимые для mvc
            services.AddMvc();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            //Добавляем расширение для использования статических файлов (скрипты, html, css файлы)
            app.UseStaticFiles();

            //Основной метод ручной обработки запроса
            //app.Use(async (ctx, next) =>
            //{
            //    if(true)
            //    {
            //        await ctx.Response.WriteAsync("Ответ");
            //    }
            //    else
            //       await next();
            //});

            //Обработка адреса
            //app.Map("/TestPath", a =>
            //{
            //    a.Run(async ctx => await ctx.Response.WriteAsync("Test"));
            //});

            //Разновидность Map
            //app.MapWhen(
            //    ctx => ctx.Request.Query.ContainsKey("TestId") && ctx.Request.Query["TestId"] == "5",
            //    a => a.Run(async ctx => await ctx.Response.WriteAsync("Test TestId = 5")));

            //Свой компонент ПО
            //app.UseMiddleware<TestMiddleware>();

            //Использовать метод расширения
            //app.UseTestMiddleware();

            //Добавляем обработку запросов в mvc-формате
            app.UseMvc(routes =>
            {
                routes.MapRoute(name: "default", template: "{controller=Home}/{action=Index}/{id?}");
            });

            //app.Run(ctx => ctx.Response.WriteAsync("Hello World")); //Middleware, который завершает конвейер

            //var replayStr = Configuration["CustomReplayString"];
            //
            //app.Run(async (context) =>
            //{
            //    //await context.Response.WriteAsync("Hello World!");
            //    await context.Response.WriteAsync(replayStr);
            //});

        }
    }
}
