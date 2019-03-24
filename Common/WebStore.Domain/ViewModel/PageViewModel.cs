using System;
using System.Collections.Generic;
using System.Text;

namespace WebStore.ViewModel
{
    /// <summary> Модель постраничного разбития </summary>
    public class PageViewModel
    {
        /// <summary> Общее количество элементов </summary>
        public int TotalItems { get; set; }

        /// <summary> Количество элементов на странице </summary>
        public int PageSize { get; set; }

        /// <summary> Текущая страница </summary>
        public int PageNumber { get; set; }

        /// <summary> Общее количество страниц </summary>
        //Ceiling возвращает наименьшее целое число, которое не меньше value
        public int TotalPages => (int)Math.Ceiling((decimal)TotalItems / PageSize);  
    }
}
