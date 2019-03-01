using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace WebStore.Models
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
