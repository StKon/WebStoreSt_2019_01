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
using WebStore.Infrastructure.Filters;
using WebStore.Infrastructure.Conventions;
using WebStore.Interfaces;
using WebStore.Services;
using WebStore.DAL.Context;
using Microsoft.EntityFrameworkCore;
using WebStore.Domain.Entities;
using Microsoft.AspNetCore.Identity;
using WebStore.Interfaces.Api;
using WebStore.Clients.Values;
using WebStore.Clients.Employees;
using WebStore.Clients.Products;
using WebStore.Clients.Orders;
using WebStore.Clients.Users;
using Microsoft.Extensions.Logging;
using WebStore.Logger;
using WebStore.Services.Middleware;

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

            // Добавляем реализацию клиента
            services.AddTransient<IValuesService, ValuesClient>();

            //регистрация сервиса работы с сотрудниками
            services.AddTransient<IEmployeesData, EmployeesClient>();

            //регистрация сервиса работы с товарами
            services.AddTransient<IProductData, ProductsClient>();

            //регистрируем сервис работы с корзиной
            services.AddScoped<ICartStore, CookieCartStore>();
            services.AddScoped<ICartService, CookieCartService>();

            //регистрируем сервис работы с заказами
            services.AddTransient<IOrdersService, OrdersClient>();

            //регистрируем контекст как сервис использую строку соединения
            //services.AddDbContext<WebStoryContext>(opt =>
            //{
            //    opt.UseSqlServer(Configuration.GetConnectionString("DefaultConnection"));
            //});

            //регистрируем авторизацию
            services.AddIdentity<User, IdentityRole>()
                //.AddEntityFrameworkStores<WebStoryContext>()
                .AddDefaultTokenProviders();

            #region identity

            services.AddTransient<IUserStore<User>, UsersClient>();
            services.AddTransient<IUserRoleStore<User>, UsersClient>();
            services.AddTransient<IUserClaimStore<User>, UsersClient>();
            services.AddTransient<IUserPasswordStore<User>, UsersClient>();
            services.AddTransient<IUserTwoFactorStore<User>, UsersClient>();
            services.AddTransient<IUserEmailStore<User>, UsersClient>();
            services.AddTransient<IUserPhoneNumberStore<User>, UsersClient>();
            services.AddTransient<IUserLoginStore<User>, UsersClient>();
            services.AddTransient<IUserLockoutStore<User>, UsersClient>();
            services.AddTransient<IRoleStore<IdentityRole>, RolesClient>();

            #endregion

            //Конфигурация пароля и пользователя
            services.Configure<IdentityOptions>(opt =>
            {
                opt.Password.RequiredLength = 6;  //пароль 6 символов
                opt.Password.RequireDigit = true; //пароль содержит хотя бы одну цифру
                opt.Password.RequiredUniqueChars = 0;  //
                opt.Password.RequireLowercase = false;
                opt.Password.RequireUppercase = false;
                opt.Password.RequireNonAlphanumeric = false;

                opt.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromMinutes(30);  //Пользователь блокируется при неправильном входе на 30 мин
                opt.Lockout.MaxFailedAccessAttempts = 10;    //Max кол-во неудачных входов в систему
                opt.Lockout.AllowedForNewUsers = true;       //разрешение для нового пользователя

                //opt.User.RequireUniqueEmail = true;   //обязательно уникальная email
            });

            //Конфигурация системы куки
            services.ConfigureApplicationCookie(opt =>
            {
                opt.Cookie.HttpOnly = true;  //только по HTTP
                opt.Cookie.Expiration = TimeSpan.FromDays(150);  //время жизни

                opt.LoginPath = "/Account/Login";   //путь входа в систему
                opt.LogoutPath = "/Account/Logout";  //путь выхода из системы
                opt.AccessDeniedPath = "/Account/AccessDenied";  //доступ запрещен

                opt.SlidingExpiration = true;  //новая куки при переходе из анонимного в авторизованный
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, ILoggerFactory loggerFactory)
        {
            loggerFactory.AddLog4Net();  //добавлена логирование log4net            

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            //Использование файлов по умолчанию
            app.UseDefaultFiles();

            //Добавляем расширение для использования статических файлов (скрипты, html, css файлы)
            app.UseStaticFiles();

            //используем аутентификацию
            app.UseAuthentication();

            //Обработка ошибок
            app.UseStatusCodePagesWithRedirects("~/home/ErrorStatus/{0}");

            //логирование исключений
            app.UseMiddleware(typeof(ErrorHandlingMiddleware));

            //Добавляем обработку запросов в mvc-формате
            app.UseMvc(routes =>
            {
                //К областям
                routes.MapRoute(name: "areas", template: "{area:exists}/{controller=Home}/{action=Index}/{id?}");

                routes.MapRoute(name: "default", template: "{controller=Home}/{action=Index}/{id?}");
            });
        }
    }
}
