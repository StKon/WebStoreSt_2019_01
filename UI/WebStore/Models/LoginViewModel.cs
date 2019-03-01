using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc;

namespace WebStore.Models
{
    public class LoginViewModel
    {
        //пользователь
        [Required, Display(Name = "Имя пользователя"), MaxLength(256)]
        public string UserName { get; set; }

        //пароль
        [Required, Display(Name = "Пароль"), DataType(DataType.Password)]
        public string Password { get; set; }
        
        [Display(Name = "Запомнить меня")]
        public bool RememberMe { get; set; }

        //откуда пользователь регистрируется
        public string ReturnUrl { get; set; }
    }
}
