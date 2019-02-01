﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebStore.Models
{
    /// <summary>
    /// Модель сотрудника
    /// </summary>
    public class EmployeeViewModel
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string SecondName { get; set; }
        public string Patronymic { get; set; }
        public int Age { get; set; }
        public DateTime BirthDate { get; set; }
        public DateTime DateWork { get; set; }
    }
}
