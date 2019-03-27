using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using WebStore.Interfaces;
using WebStore.ViewModels;
using WebStore.Domain.Entities;
using WebStore.Domain.Entities.Filters;
using Microsoft.Extensions.Configuration;
using WebStore.ViewModel;

namespace WebStore.Controllers
{
    public class CatalogController : Controller
    {
        private readonly IProductData _productData;
        private readonly IConfiguration _configuration;

        /// <summary> Конструктор </summary>
        /// <param name="productData"></param>
        public CatalogController(IProductData productData, IConfiguration configuration)
        {
            _productData = productData;
            _configuration = configuration;
        }

        public IActionResult Shop(int? sectionId, int? brandId, int page = 1)
        {
            int page_size = int.Parse(_configuration["PageSize"]);

            var products = _productData.GetProducts(new ProductFilter
            {
                BrandId = brandId,
                SectionId = sectionId,
                Page = page,
                PageSize = page_size
            });

            var catalog_view = new CatalogViewModel()
            {
                BrandId = brandId,
                SectionId = sectionId,
                Products = products.Products.Select(p => new ProductViewModel()
                {
                    Id = p.Id,
                    ImageUrl = p.ImageUrl,
                    Name = p.Name,
                    Order = p.Order,
                    Price = p.Price,
                    BrandId = p.Brand?.Id,
                    Brand = p.Brand is null ? string.Empty : p.Brand.Name,
                    SectionId = p.Section.Id,
                    Section = p.Section?.Name ?? string.Empty
                }).ToList(),
                PageModel = new PageViewModel
                {
                    PageNumber = page,
                    PageSize = page_size,
                    TotalItems = products.TotalCount
                }
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
                BrandId = product.Brand?.Id,
                Brand = product.Brand?.Name ?? string.Empty,  
                SectionId = product.Section?.Id ?? 0,
                Section = product.Section?.Name ?? string.Empty
            });            
        }

        /// <summary> получить частичное представление страницы товаров с фильтрами </summary>
        public IActionResult GetFilteredItems(int? sectionId, int? brandId, int page = 1)
        {
            var productsModel = GetProducts(sectionId, brandId, page, out var totalCount);
            return PartialView("Partial/_FeaturesItems", productsModel);
        }

        /// <summary> получить отфильтрованные товары </summary>
        private IEnumerable<ProductViewModel> GetProducts(int? sectionId, int? brandId, int page, out int totalCount)
        {
            var products = _productData.GetProducts(new ProductFilter
            {
                BrandId = brandId,
                SectionId = sectionId,
                Page = page,
                PageSize = int.Parse(_configuration["PageSize"])
            });

            totalCount = products.TotalCount;
            return products.Products.Select(p => new ProductViewModel()
            {
                Id = p.Id,
                ImageUrl = p.ImageUrl,
                Name = p.Name,
                Order = p.Order,
                Price = p.Price,
                BrandId = p.Brand?.Id ?? 0,
                Brand = p.Brand?.Name ?? string.Empty,
                SectionId = p.Section?.Id ?? 0,
                Section = p.Section?.Name ?? string.Empty
            }).ToList();
        }
    }
}