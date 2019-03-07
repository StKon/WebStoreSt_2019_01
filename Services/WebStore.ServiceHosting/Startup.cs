using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using WebStore.DAL.Context;
using WebStore.Domain.Entities;
using WebStore.Interfaces;
using WebStore.Services;

namespace WebStore.ServiceHosting
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
            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_1);

            //регистрируем контекст как сервис использую строку соединения
            services.AddDbContext<WebStoryContext>(options => options.UseSqlServer(Configuration.GetConnectionString("DefaultConnection")));

            //регистрация сервиса работы с сотрудниками
            services.AddSingleton<IEmployeesData, InMemoryEmployeesData>();   //один объект на всю систему  

            //регистрация сервиса работы с товарами
            //services.AddSingleton<IProductData, InMemoryProductData>();   //один объект на всю систему         

            //регистрируем сервис работы с товарами SQLProductData
            services.AddScoped<IProductData, SQLProductData>();

            //регистрируем сервис работы с заказами
            services.AddScoped<IOrdersService, SQLOrdersService>();

            //регистрируем сервис работы с корзиной
            services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
            services.AddScoped<ICartService, CookieCartService>();

            //регистрируем авторизацию
            services.AddIdentity<User, IdentityRole>().AddEntityFrameworkStores<WebStoryContext>().AddDefaultTokenProviders();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseMvc();
        }
    }
}
