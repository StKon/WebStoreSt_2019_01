using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using WebStore.DAL.Context;
using WebStore.Domain.Entities;

namespace WebStore.Data
{
    /// <summary> Начальные данные </summary>
    internal static class DbInitializer
    {
        /// <summary> Инициализация БД </summary>
        public static void Initialize(this WebStoryContext context)
        {
            context.Database.EnsureCreated();   //база есть

            if (context.Products.Any()) return;  //нет продуктов в БД

            using (var transaction = context.Database.BeginTransaction())
            {
                foreach (var section in TestData.Sections)
                    context.Sections.Add(section);

                context.Database.ExecuteSqlCommand("SET IDENTITY_INSERT [dbo].[Sections] ON");
                context.SaveChanges();
                context.Database.ExecuteSqlCommand("SET IDENTITY_INSERT [dbo].[Sections] OFF");
                transaction.Commit();
            }

            using (var transaction = context.Database.BeginTransaction())
            {
                foreach (var brand in TestData.Brands)
                    context.Brands.Add(brand);

                context.Database.ExecuteSqlCommand("SET IDENTITY_INSERT [dbo].[Brands] ON");
                context.SaveChanges();
                context.Database.ExecuteSqlCommand("SET IDENTITY_INSERT [dbo].[Brands] OFF");
                transaction.Commit();
            }

            using (var transaction = context.Database.BeginTransaction())
            {
                foreach (var product in TestData.Products)
                    context.Products.Add(product);

                context.Database.ExecuteSqlCommand("SET IDENTITY_INSERT [dbo].[Products] ON");
                context.SaveChanges();
                context.Database.ExecuteSqlCommand("SET IDENTITY_INSERT [dbo].[Products] OFF");
                transaction.Commit();
            }
        }

        /// <summary> Инициализация авторизации </summary>
        public static async Task InitializeIdentityAsync(this IServiceProvider services )
        {
            //Роль-менеджер
            var role_manager = services.GetService<RoleManager<IdentityRole>>();

            //роль пользователя
            if(! await role_manager.RoleExistsAsync(User.UserRole))
            {
                //добавление роли
                await role_manager.CreateAsync(new IdentityRole(User.UserRole));
            }

            //роль администратора
            if (! await role_manager.RoleExistsAsync(User.AdminRole))
            {
                //добавление роли
                await role_manager.CreateAsync(new IdentityRole(User.AdminRole));
            }

            //пользователь- менеджер
            var user_manager = services.GetService<UserManager<User>>();
            //хранилище пользователей
            var user_store = services.GetService<IUserStore<User>>();
            //администратор пользователь
            if (await user_store.FindByNameAsync(User.AdminUser, CancellationToken.None) == null)  //ищем пользователя
            {
                //пользователь
                var admin = new User
                {
                    UserName = User.AdminUser,
                    Email = $"{User.AdminUser}.mail.ru"
                };

                //добавление пользователя
                if((await user_manager.CreateAsync(admin,"123456")).Succeeded)
                {
                    //добавляем роль администратора
                    await user_manager.AddToRoleAsync(admin, User.AdminRole);
                }
            }
        }
    }
}
