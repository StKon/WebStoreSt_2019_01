using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebStore.Domain.Entities.Filters
{
    /// <summary> Класс для фильтрации товаров </summary>
    public class ProductFilter
    {
        /// <summary> Секция, к которой принадлежит товар </summary>
        public int? SectionId;

        /// <summary> Бренд товара </summary>
        public int? BrandId;

        /// <summary> Id товаров, которые надо отобразить </summary>
        public IEnumerable<int> Ids { get; set; }

        /// <summary> Текущая страница </summary>
        public int Page { get; set; }

        /// <summary> Количество элементов на странице </summary>
        public int? PageSize { get; set; }
    }
}
