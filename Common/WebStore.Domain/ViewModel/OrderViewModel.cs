using System.ComponentModel.DataAnnotations;

namespace WebStore.ViewModels
{
    public class OrderViewModel
    {
        [Required, Display(Name = "Название заказа")]
        public string Name { get; set; }

        [Required, DataType(DataType.PhoneNumber), Display(Name = "Телефон")]
        public string Phone { get; set; }

        [Required, Display(Name = "Адрес")]
        public string Address { get; set; }
    }
}
