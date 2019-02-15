﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebStory.Domain.Entities;
using WebStore.Infrastructure.Filters;

namespace WebStore.Infrastructure.Interfaces
{
    /// <summary> Работа с товарами </summary>
    public interface IProductData
    {
        /// <summary> Возвращает бренды </summary>
        IEnumerable<Brand> GetBrands();

        /// <summary> Возвращает секции </summary>
        IEnumerable<Section> GetSections();

        /// <summary> Возвращает товары </summary>
        /// <returns></returns>
        IEnumerable<Product> GetProducts(ProductFilter productFilter);

        /// <summary> Кол-во товаров бренда </summary>
        int GetBrandProductCount(int brandId);
    }
}
