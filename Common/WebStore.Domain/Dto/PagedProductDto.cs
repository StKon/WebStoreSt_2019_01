using System;
using System.Collections.Generic;
using System.Text;

namespace WebStore.Domain.Dto
{
    /// <summary> Передача товаров на странице </summary>
    public class PagedProductDto
    {
        /// <summary> Выборка товаов для текущей страницы </summary>
        public IEnumerable<ProductDto> Products { get; set; }

        /// <summary> Общее количество товаров в запросе </summary>
        public int TotalCount { get; set; }
    }
}
