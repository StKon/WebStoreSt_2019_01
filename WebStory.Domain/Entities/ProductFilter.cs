using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebStory.Domain.Entities
{
    /// <summary> Класс для фильтрации товаров </summary>
    public class ProductFilter
    {
        /// <summary> Секция, к которой принадлежит товар </summary>
        public int? SectionId;

        /// <summary> Бренд товара </summary>
        public int? BrandId;
    }
}
