using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc;

namespace WebStore.Models
{
    /// <summary>
    /// Модель сотрудника
    /// </summary>
    public class EmployeeViewModel
    {
        [Display(Name = "ID")]
        //[HiddenInput(DisplayValue =false)]        
        public int Id { get; set; }

        [Display(Name = "Имя")]
        [Required(AllowEmptyStrings = false, ErrorMessage = "Имя обязательно")]
        [RegularExpression("^[A-ZА-Я][a-zа-я]*", ErrorMessage ="Имя должно начинаться с заглавной буквы и содержать только буквы")]
        public string FirstName { get; set; }

        [Display(Name = "Фамилия")]
        [Required(AllowEmptyStrings = false, ErrorMessage = "Фамилия обязательна")]
        public string SecondName { get; set; }

        [Display(Name = "Отчество")]
        public string Patronymic { get; set; }

        [Display(Name = "Возраст")]
        [Required(ErrorMessage = "Возраст обязателен")]
        [Range(minimum: 18, maximum: 80, ErrorMessage ="Возраст должен быть от 18 до 80")]
        public int Age { get; set; }

        [Display(Name = "Дата рождения")]
        [Required(ErrorMessage = "Дата рождения обязательна")]
        [DataType(DataType.Date)]
        public DateTime BirthDate { get; set; }

        [Display(Name = "Дата устройства на работу")]
        [Required(ErrorMessage = "Дата устройства на работу обязательна")]
        [DataType(DataType.Date)]
        //[UIHint("ValueView")]  //шаблон
        public DateTime DateWork { get; set; }
    }
}
