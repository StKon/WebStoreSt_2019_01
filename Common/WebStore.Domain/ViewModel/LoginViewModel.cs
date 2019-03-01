using System.ComponentModel.DataAnnotations;

namespace WebStore.ViewModels
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
