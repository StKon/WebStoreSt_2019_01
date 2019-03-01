using System.ComponentModel.DataAnnotations;

namespace WebStore.ViewModels
{
    /// <summary> Регистрация пользователя </summary>
    public class RegisterUserViewModel
    {
        //пользователь
        [Required, Display(Name ="Имя пользователя"), MaxLength(256)]
        public string UserName { get; set; }

        //пароль
        [Required, Display(Name = "Пароль"), DataType(DataType.Password)]
        public string Password { get; set; }

        //подтверждение пароля
        [Required, Display(Name = "Подтверждение пароля"), DataType(DataType.Password), Compare(nameof(Password))]
        public string ConfirmPassword { get; set; }
    }
}
