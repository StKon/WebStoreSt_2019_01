using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using WebStore.Infrastructure.Interfaces;
using WebStore.Models;
using WebStore.Domain.Entities;
using WebStore.Domain.Entities.Filters;

namespace WebStore.Controllers
{
    public class CatalogController : Controller
    {
        private readonly IProductData _productData;

        /// <summary> Конструктор </summary>
        /// <param name="productData"></param>
        public CatalogController(IProductData productData)
        {
            _productData = productData;
        }

        public IActionResult Shop(int? sectionId, int? brandId)
        {
            var products = _productData.GetProducts(new ProductFilter
            {
                BrandId = brandId,
                SectionId = sectionId
            });

            var catalog_view = new CatalogViewModel()
            {
                BrandId = brandId,
                SectionId = sectionId,
                Products = products.Select(p => new ProductViewModel()
                {
                    Id = p.Id,
                    ImageUrl = p.ImageUrl,
                    Name = p.Name,
                    Order = p.Order,
                    Price = p.Price,
                    BrandId = p.BrandId,
                    Brand = p.Brand is null ? string.Empty : p.Brand.Name,
                    SectionId = p.SectionId,
                    Section = p.Section?.Name ?? string.Empty
                }).ToList()
            };
            catalog_view.Products.OrderBy(p => p.Order);
            return View(catalog_view);
        }

        public IActionResult ProductDetails(int id)
        {
            var product = _productData.GetProductById(id);

            if (product is null)
                return NotFound();

            return View(new ProductViewModel
            {
                Id = product.Id,
                Name = product.Name,
                ImageUrl = product.ImageUrl,
                Order = product.Order,
                Price = product.Price,
                BrandId = product.BrandId,
                Brand = product.Brand?.Name ?? string.Empty,
                SectionId = product.SectionId,
                Section = product.Section?.Name ?? string.Empty
            });            
        }
    }
}