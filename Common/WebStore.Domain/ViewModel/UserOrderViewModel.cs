using System.ComponentModel.DataAnnotations;

namespace WebStore.ViewModels
{
    /// <summary> Заказы пользователя </summary>
    public class UserOrderViewModel
    {
        [Display(Name = "ID")]
        public int Id { get; set; }

        [Display(Name = "Название заказа")]
        public string Name { get; set; }

        [Display(Name = "Телефон")]
        public string Phone { get; set; }

        [Display(Name = "Адрес")]
        public string Address { get; set; }

        [Display(Name = "Сумма заказа")]
        public decimal TotalSum { get; set; }
    }
}
