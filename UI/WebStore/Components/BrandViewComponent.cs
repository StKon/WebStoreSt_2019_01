using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using WebStore.Interfaces;
using WebStore.ViewModels;
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

        public async Task<IViewComponentResult> InvokeAsync(string brandId)
        {
            var _brandId = int.TryParse(brandId, out var id) ? id : (int?)null;  //выбранный бренд

            var brand = GetBrands(_brandId);
            return View(new BrandCompleteViewModel
            {
                Brands = brand,
                CurrentBrandId = _brandId
            });
        }

        private List<BrandViewModel> GetBrands(int? brandId)
        {
            var brands = _productData.GetBrands();
            var brand_views = brands.Select(br => new BrandViewModel
            {
                Id = br.Id,
                Name = br.Name,
                Order = br.Order,
                ProductsCount = _productData.GetBrandProductCount(br.Id)
            }).ToList();

            //сортировка списка
            brand_views.Sort((a, b) => Comparer<int>.Default.Compare(a.Order, b.Order));

            return brand_views;
        }
    }
}
