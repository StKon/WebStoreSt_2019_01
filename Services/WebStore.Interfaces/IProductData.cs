﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebStore.Domain.Dto;
using WebStore.Domain.Entities;
using WebStore.Domain.Entities.Filters;

namespace WebStore.Interfaces
{
    /// <summary> Работа с товарами </summary>
    public interface IProductData
    {
        /// <summary> Возвращает бренды </summary>
        IEnumerable<Brand> GetBrands();

        /// <summary> Бренд по Id </summary>
        Brand GetBrandById(int id);

        /// <summary> Возвращает секции </summary>
        IEnumerable<Section> GetSections();

        /// <summary> Секция по Id </summary>
        Section GetSectionById(int id);

        /// <summary> Возвращает товары </summary>
        /// <returns></returns>
        //IEnumerable<ProductDto> GetProducts(ProductFilter productFilter = null);

        /// <summary> Список товаров с постраничным разбиением </summary>
        PagedProductDto GetProducts(ProductFilter filter = null);

        /// <summary> Товар по индексу </summary>
        ProductDto GetProductById(int id);

        /// <summary> Кол-во товаров бренда </summary>
        int GetBrandProductCount(int brandId);

        /// <summary> Изменить товар </summary>
        ProductDto UpdateProduct(ProductDto prod);

        /// <summary> Добавить товар </summary>
        ProductDto AddProduct(ProductDto prod);

        /// <summary> Удалить товар </summary>
        void DeleteProduct(int id);
    }
}
