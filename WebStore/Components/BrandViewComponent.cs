using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using WebStore.Infrastructure.Interfaces;
using WebStore.Models;
using WebStore.Domain.Entities;

namespace WebStore.Components
{
    public class BrandViewComponent : ViewComponent
    {
        private readonly IProductData _productData;

        /// <summary> Компонент представления брендов товара </summary>
        public BrandViewComponent(IProductData productData)
        {
            _productData = productData;
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            var brand = GetBrands();
            return View(brand);
        }

        private List<BrandViewModel> GetBrands()
        {
            var brands = _productData.GetBrands();
            var brand_views = brands.Select(br => new BrandViewModel
            {
                Id = br.Id,
                Name = br.Name,
                Order = br.Order,
                ProductsCount =  _productData.GetBrandProductCount(br.Id)
            }).ToList();
            //сортировка списка
            brand_views.Sort((a, b) => Comparer<int>.Default.Compare(a.Order, b.Order));
            return brand_views;
        }
    }
}
