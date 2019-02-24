using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.AspNetCore.Identity;

namespace WebStore.Domain.Entities
{
    /// <summary> Пользователи </summary>
    public class User : IdentityUser
    {
        public const string UserRole = "User";  //пользователь
        public const string AdminRole = "Admin";  //администратор
        public const string AdminUser = "Admin";  //администратор пользователь

    }
}
